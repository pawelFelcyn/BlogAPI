using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using API;
using System.Linq;
using Application.Services;
using Moq;
using Application.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using FluentAssertions;
using System.Collections.Generic;
using API.Tests.Helpers;
using System;
using Domain.Exceptions;
using System.Net;

namespace API.Tests.Controllers;

public class AccountConntrollerTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<LoginDtoFactory>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private const string JWT_TOKEN = "jwt token";
    private readonly Mock<IAccountService> _accountServiceMock;
    private readonly Dictionary<LoginRequest, Action<Mock<IAccountService>>> _mockCfgs;
    private readonly LoginDtoFactory _loginDtoFactory;

    public AccountConntrollerTests(WebApplicationFactory<Program> factory, LoginDtoFactory loginDtoFactory)
    {
        _accountServiceMock = new Mock<IAccountService>();

        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var accountService = services.SingleOrDefault(s => s.ServiceType == typeof(IAccountService));
                services.Remove(accountService);

                services.AddSingleton(_accountServiceMock.Object);
            });
        });

        _client = _factory.CreateClient();

        _mockCfgs = GetMockConfigurations();
        _loginDtoFactory = loginDtoFactory;
    }

    [Theory]
    [InlineData(LoginRequest.InvalidEmail, HttpStatusCode.BadRequest)]
    [InlineData(LoginRequest.InvalidPassword, HttpStatusCode.BadRequest)]
    [InlineData(LoginRequest.InvalidBody, HttpStatusCode.BadRequest)]
    [InlineData(LoginRequest.EverythingOk, HttpStatusCode.OK)]
    public async Task Login_ForInvalidBody_ReturnsBadRequestStatusCode(LoginRequest request, HttpStatusCode expectedStatusCode)
    {
        var dto = _loginDtoFactory.CreateLoginDto(request);
        var json = JsonConvert.SerializeObject(dto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _mockCfgs[request]?.Invoke(_accountServiceMock);
        var result = await _client.PostAsync("api/Accounts/login", content);

        result.StatusCode.Should().Be(expectedStatusCode);
    }

    private Dictionary<LoginRequest, Action<Mock<IAccountService>>>GetMockConfigurations()
    {
        return new()
        {
            { LoginRequest.InvalidBody, null },
            { LoginRequest.InvalidEmail, mock => mock.Setup(m => m.GetToken(It.IsAny<LoginDto>())).Throws<InvalidEmailException>()},
            { LoginRequest.InvalidPassword, mock => mock.Setup(m => m.GetToken(It.IsAny<LoginDto>())).Throws<InvalidPasswordException>()},
            { LoginRequest.EverythingOk, mock => mock.Setup(m => m.GetToken(It.IsAny<LoginDto>())).Returns(JWT_TOKEN)}
        };
    }
}
