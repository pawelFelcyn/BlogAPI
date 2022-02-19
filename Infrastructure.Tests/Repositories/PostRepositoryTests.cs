using Domain.Entities;
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
        //var post = new Post()
        //{
        //    Title = "",
        //    Content = "",
        //    Created = System.DateTime.UtcNow,
        //    LastModyfied = System.DateTime.Now,
        //    CreatedById = 0
        //};
        var posts = _postRepository.GetAll();

        posts.Should().BeSameAs(_dbContext.Posts);
    }
}
