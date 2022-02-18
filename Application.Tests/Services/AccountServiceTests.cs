using Application.Authentication;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;
using Application.Services;
using Xunit;
using Application.Dtos;
using FluentAssertions;
using Domain.Exceptions;

namespace Application.Tests.Services;

public class AccountServiceTests
{
    private readonly Mock<IAccountRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IPasswordHasher<User>> _passwordHasherMock;
    private readonly Mock<ITokenGenerator> _tokenGeneratorMock;
    private readonly AccountService _accountService;

    public AccountServiceTests()
    {
        _repositoryMock = new();
        _mapperMock = new();
        _passwordHasherMock = new();
        _tokenGeneratorMock = new();
        _accountService = new(_repositoryMock.Object, _mapperMock.Object, _passwordHasherMock.Object, _tokenGeneratorMock.Object);
    }

    [Fact]
    public void GetToken_ForLoginDto_ReturnsToken()
    {
        var dto = new LoginDto(null, null);
        var user = new User();
        var token = "jwtToken";

        _repositoryMock.Setup(m => m.GetByEmail(It.IsAny<string>())).Returns(user);
        _passwordHasherMock.Setup(m => m.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(PasswordVerificationResult.Success);
        _tokenGeneratorMock.Setup(m => m.GetTokenStr(It.IsAny<User>())).Returns(token);

        var result = _accountService.GetToken(dto);

        result.Should().Be(token);
    }

    [Fact]
    public void GetToken_ForBadpassword_ThrowsInvalidPasswordException()
    {
        var dto = new LoginDto(null, null);
        var user = new User();

        _repositoryMock.Setup(m => m.GetByEmail(It.IsAny<string>())).Returns(user);
        _passwordHasherMock.Setup(m => m.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(PasswordVerificationResult.Failed);

        var action = () => _accountService.GetToken(dto);

        Assert.Throws<InvalidPasswordException>(action);
    }
}
