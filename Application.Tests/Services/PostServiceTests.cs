using Application.Dtos;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Xunit;

namespace Application.Tests.Services;

public class PostServiceTests
{
    private readonly Mock<IPostRepository> _postRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IUserContextService> _userContextServiceMock;
    private readonly Mock<IAuthorizationService> _authorizationServiceMock;
    private readonly PostService _postService;

    public PostServiceTests()
    {
        _postRepositoryMock = new();
        _mapperMock = new();
        _userContextServiceMock = new();
        _authorizationServiceMock = new();

        _postService = new(_postRepositoryMock.Object, _mapperMock.Object, _userContextServiceMock.Object,
            _authorizationServiceMock.Object);
    }

    [Fact]
    public void GetById_ForGivenId_ReturnsPostDto()
    {
        var postDto = new PostDetailsDto();
        var post = new Post();

        _mapperMock.Setup(m => m.Map<PostDetailsDto>(It.IsAny<Post>())).Returns(postDto);
        _postRepositoryMock.Setup(m => m.GetById(It.IsAny<int>())).Returns(post);

        var result = _postService.GetById(0);

        result.Should().Be(postDto);
    }
}
