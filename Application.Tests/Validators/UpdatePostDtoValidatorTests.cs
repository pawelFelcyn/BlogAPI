using Application.Dtos;
using Application.Tests.Helpers;
using Application.Validators;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Application.Tests.Validators;

public class UpdatePostDtoValidatorTests : IClassFixture<UpdatePostDtoValidator>
{
    private readonly UpdatePostDtoValidator _validator;
    private readonly ITestOutputHelper _outputHelper;

    public UpdatePostDtoValidatorTests(UpdatePostDtoValidator validator, ITestOutputHelper outputHelper)
    {
        _validator = validator;
        _outputHelper = outputHelper;
    }

    public static IEnumerable<object[]> ValidateGetTestData()
    {
        yield return new object[] { new UpdatePostDto("Content"), true };
        yield return new object[] { new UpdatePostDto(StringHelper.GetStringWithLength(5000)), true };
        yield return new object[] { new UpdatePostDto(StringHelper.GetStringWithLength(5001)), false };
        yield return new object[] { new UpdatePostDto(""), false };
        yield return new object[] { new UpdatePostDto(null), false };
    }

    [Theory]
    [MemberData(nameof(ValidateGetTestData))]
    public void Validate_ForGivenModel_ReturnsProperValidationResult(UpdatePostDto model, bool expectedIsValid)
    {
        var result = _validator.Validate(model);

        foreach (var e in result.Errors)
        {
            _outputHelper.WriteLine(e.ErrorMessage);
        }

        result.IsValid.Should().Be(expectedIsValid);
    }
}
