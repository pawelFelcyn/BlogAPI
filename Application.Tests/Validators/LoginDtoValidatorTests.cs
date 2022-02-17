using Application.Dtos;
using Application.Validators;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Application.Tests.Validators;

public class LoginDtoValidatorTests : IClassFixture<LoginDtoValiodator>
{
    private readonly LoginDtoValiodator _vaildator;
    private ITestOutputHelper _testOutputHelper;

    public LoginDtoValidatorTests(LoginDtoValiodator vaildator, ITestOutputHelper testOutputHelper)
    {
        _vaildator = vaildator;
        _testOutputHelper = testOutputHelper;
    }

    public static IEnumerable<object> ValidateTestData()
    {
        yield return new object[] { new LoginDto("email", "password"), true };
        yield return new object[] { new LoginDto("email", ""), false };
        yield return new object[] { new LoginDto("email", null), false };
        yield return new object[] { new LoginDto("", "password"), false };
        yield return new object[] { new LoginDto(null, "password"), false };
    }

    [Theory]
    [MemberData(nameof(ValidateTestData))]
    public void Validate_ForGivenModel_ReturnsProperValidationResult(LoginDto model, bool expectedResult)
    {
        var result = _vaildator.Validate(model);

        foreach (var e in result.Errors)
        {
            _testOutputHelper.WriteLine(e.ErrorMessage);
        }

        result.IsValid.Should().Be(expectedResult);
    }
}
