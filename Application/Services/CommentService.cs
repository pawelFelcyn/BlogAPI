using Application.Authorization;
using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _repository;
    private readonly IUserContextService _userContextService;
    private readonly IAuthorizationService _authorizationService;
    private readonly IMapper _mapper;

    public CommentService(ICommentRepository repository, IUserContextService userContextService,
        IAuthorizationService authorizationService, IMapper mapper)
    {
        _repository = repository;
        _userContextService = userContextService;
        _authorizationService = authorizationService;
        _mapper = mapper;
    }

    public IEnumerable<CommentDto> GetAll(int postId)
    {
        var comments = _repository.GetAll(postId);

        return _mapper.Map<IEnumerable<CommentDto>>(comments);
    }

    public CommentDto GetById(int postId, int commentId)
    {
        var comment = _repository.GetById(postId, commentId);

        return _mapper.Map<CommentDto>(comment);
    }

    public CommentDto Create(int postId, CreateCommentDto dto)
    {
        var comment = _mapper.Map<Comment>(dto);
        comment.CreatedById = _userContextService.GetId;
        comment.PostId = postId;
        comment = _repository.Add(comment);

        return _mapper.Map<CommentDto>(comment);
    }

    public CommentDto Update(int postId, int commentId, UpdateCommentDto dto)
    {
        var comment = _repository.GetById(postId, commentId);

        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, comment,
            new CommentOperationRequirement(CommentOperationType.Update)).Result;

        if (!authorizationResult.Succeeded)
        {
            throw new CantUpdateCommentException();
        }

        comment = _repository.Update(comment, dto.Content);

        return _mapper.Map<CommentDto>(comment);
    }

    public void Delete(int postId, int commentId)
    {
        var comment = _repository.GetById(postId, commentId);

        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, comment,
            new CommentOperationRequirement(CommentOperationType.Delete)).Result;

        if (!authorizationResult.Succeeded)
        {
            throw new CantRemoveCommentException();
        }

        _repository.Remove(comment);
    }
}
