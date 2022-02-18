using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Infrastructure.Tests.Repositories;

public class AccountRepositoryTests 
{
    private readonly AccountRepository _repository;
    private readonly BlogDbContext _context;
    
    public AccountRepositoryTests()
    {
        var builder = new DbContextOptionsBuilder();
        //var modelBuilder = new ModelBuilder();
        //modelBuilder.Entity<User>();
        //builder.UseModel(modelBuilder.FinalizeModel());
        builder.UseInMemoryDatabase("BlogDb");
        _context = new BlogDbContext(builder.Options);
        _repository = new AccountRepository(_context);
    }

    [Fact]
    public void GetByEmail_ForCorrentEmail_ReturnsProperUser()
    {
        var user = GetUser();
        _context.Users.Add(user);
        _context.SaveChanges();

        var result = _repository.GetByEmail(user.Email);

        result.Should().Be(user);
    }

    [Fact]
    public void GetByEmail_ForIncorrectEmail_ThrowsInvalidEmailException()
    {
        var action = () => _repository.GetByEmail("invalid email");

        Assert.Throws<InvalidEmailException>(action);
    }

    [Fact]
    public void Add_ForGivenUser_AddsItToDatabase()
    {
        var user = GetUser();

        _repository.Add(user);

        _context.Users.Should().Contain(user);
    }

    private User GetUser()
        => new User()
        {
            Email = "email",
            FirstName = "",
            LastName = "",
            Nick = "",
            Role = "",
            PasswordHash = "",
            DateOfAppending = System.DateTime.UtcNow
        };
}
