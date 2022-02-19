using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Infrastructure.Tests.Repositories;

public class PostRepositoryTests
{
    private readonly PostRepository _postRepository;
    private readonly BlogDbContext _dbContext;

    public PostRepositoryTests()
    {
        var builder = new DbContextOptionsBuilder();
        builder.UseInMemoryDatabase("BlogDb");
        _dbContext = new(builder.Options);
        _postRepository = new(_dbContext);
    }

    [Fact]
    public void GetAll_ReturnsAllPosts()
    {
        var posts = _postRepository.GetAll();

        posts.Should().BeSameAs(_dbContext.Posts);
    }

    [Fact]
    public void GetById_ForNonexistingPost_ThrowsPostNotFoundException()
    {
        var action = () => _postRepository.GetById(-1);

        Assert.Throws<PostNotFoundException>(action);
    }

    [Fact]
    public void GetById_ForGivenId_ReturnsProperPost()
    {
        var post = SeedPost();

        var result = _postRepository.GetById(post.Id);

        result.Should().Be(post);
    }

    private Post SeedPost()
    {
        var post = new Post()
        {
            Title = "",
            Content = "",
            Created = System.DateTime.UtcNow,
            LastModyfied = System.DateTime.Now,
            CreatedById = 0
        };

        _dbContext.Posts.Add(post);
        _dbContext.SaveChanges();

        return post;
    }
}
