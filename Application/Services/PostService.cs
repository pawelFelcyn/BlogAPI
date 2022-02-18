using Application.Authorization;
using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Application.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUserContextService _userContextService;
    private readonly IAuthorizationService _authorizationService;

    public PostService(IPostRepository repository, IMapper mapper, IUserContextService userContextService,
        IAuthorizationService authorizationService)
    {
        _repository = repository;
        _mapper = mapper;
        _userContextService = userContextService;
        _authorizationService = authorizationService;
    }

    public IEnumerable<PostDto> GetAll()
    {
        var posts = _repository.GetAll();

        return _mapper.Map<IEnumerable<PostDto>>(posts);
    }

    public PostDetailsDto GetById(int id)
    {
        var post = _repository.GetById(id);

        return _mapper.Map<PostDetailsDto>(post);
    }

    public PostDetailsDto Create(CreatePostDto dto)
    {
        var post = _mapper.Map<Post>(dto);
        post.CreatedById = _userContextService.GetId;
        post = _repository.Add(post);

        return _mapper.Map<PostDetailsDto>(post);
    }

    public PostDetailsDto Update(int id, UpdatePostDto dto)
    {
        var post = _repository.GetById(id);

        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, post, new PostOperationRequirement()).Result;

        if (!authorizationResult.Succeeded)
        {
            throw new CantUpdatePostException();
        }

        post = _repository.Update(post, dto.Content);

        return _mapper.Map<PostDetailsDto>(post);
    }

    public void Delete(int id)
    {
        var post = _repository.GetById(id);

        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, post, new PostOperationRequirement()).Result;

        if (!authorizationResult.Succeeded)
        {
            throw new CantDeletePostException();
        }

        _repository.Remove(post);
    }
}
