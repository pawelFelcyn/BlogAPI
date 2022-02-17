using Application.Dtos;
using Application.Validators;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Application.Tests.Validators;

public class RegisterDtoValidatorTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly Mock<IEmailValidationHelper> _emailValidationHelper;
    private readonly RegisterDtoValidator _validator;

    public RegisterDtoValidatorTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _emailValidationHelper = new();
        _validator = new(_emailValidationHelper.Object);
    }

    public static IEnumerable<object[]> ValidateGetData()
    {
        yield return new object[] {new RegisterDto("Name", "Name", "Nick",
            "Admin", "email@email.com", "Password", "Password"), false, true};

        yield return new object[] {new RegisterDto("Name", "Name", "Nick",
            "Admin", "email@email.com", "Password", "Password"), true, false};

        yield return new object[] {new RegisterDto("", "Name", "Nick",
            "Admin", "email@email.com", "Password", "Password"), false, false};

        yield return new object[] {new RegisterDto(null, "Name", "Nick",
            "Admin", "email@email.com", "Password", "Password"), false, false};
        
        yield return new object[] {new RegisterDto("Tooooo loooooooooonng", "Name", "Nick",
            "Admin", "email@email.com", "Password", "Password"), false, false};

        yield return new object[] {new RegisterDto("Name", "", "Nick",
            "Admin", "email@email.com", "Password", "Password"), false, false};

        yield return new object[] {new RegisterDto("Name", null, "Nick",
            "Admin", "email@email.com", "Password", "Password"), false, false};

        yield return new object[] {new RegisterDto("Name", "Toooooo loooooooooooooooooooong", "Nick",
            "Admin", "email@email.com", "Password", "Password"), false, false};

        yield return new object[] {new RegisterDto("Name", "Name", "",
            "Admin", "email@email.com", "Password", "Password"), false, false};

        yield return new object[] {new RegisterDto("Name", "Name", null,
            "Admin", "email@email.com", "Password", "Password"), false, false};

        yield return new object[] {new RegisterDto("Name", "Name", "Toooooo loooooooooooooooooooooooooong",
            "Admin", "email@email.com", "Password", "Password"), false, false};

        yield return new object[] {new RegisterDto("Name", "Name", "Nick",
            "xd", "email@email.com", "Password", "Password"), false, false};

        yield return new object[] {new RegisterDto("Name", "Name", "Nick",
            "", "email@email.com", "Password", "Password"), false, false};

        yield return new object[] {new RegisterDto("Name", "Name", "Nick",
            null, "email@email.com", "Password", "Password"), false, false};

        yield return new object[] {new RegisterDto("Name", "Name", "Nick",
            "Admin", "xd", "Password", "Password"), false, false};

        yield return new object[] {new RegisterDto("Name", "Name", "Nick",
            "Admin", null, "Password", "Password"), false, false};

        yield return new object[] {new RegisterDto("Name", "Name", "Nick",
            "Admin", "email@email.com", "short", "short"), false, false};

        yield return new object[] {new RegisterDto("Name", "Name", "Nick",
            "Admin", "email@email.com", "Password", "Passwor"), false, false};

        yield return new object[] {new RegisterDto("Name", "Name", "Nick",
            "Admin", "email@email.com", "Password", ""), false, false};

        yield return new object[] {new RegisterDto("Name", "Name", "Nick",
            "Admin", "email@email.com", "Password", null), false, false};
    }

    [Theory]
    [MemberData(nameof(ValidateGetData))]
    public void Validate_ForValidModel_ValidationIsSucceed(RegisterDto model, bool emailTaken, bool isValid)
    {
        _emailValidationHelper.Setup(m => m.IsTaken(It.IsAny<string>())).Returns(emailTaken);
        var result = _validator.Validate(model);

        foreach (var e in result.Errors)
        {
            _testOutputHelper.WriteLine(e.ErrorMessage);
        }

        result.IsValid.Should().Be(isValid);
    }
}
