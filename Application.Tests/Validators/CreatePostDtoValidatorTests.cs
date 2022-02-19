using Application.Dtos;
using Application.Validators;
using FluentAssertions;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Application.Tests.Validators;

public class CreatePostDtoValidatorTests : IClassFixture<CreatePostDtoValidator>
{
    private readonly CreatePostDtoValidator _validator;
    private readonly ITestOutputHelper _outputHelper;

    public CreatePostDtoValidatorTests(CreatePostDtoValidator validator, ITestOutputHelper outputHelper)
    {
        _validator = validator;
        _outputHelper = outputHelper;
    }

    public static IEnumerable<object[]> ValidateGetTestData()
    {
        yield return new object[] { new CreatePostDto("Title", "Content"), true };
        yield return new object[] { new CreatePostDto(GetStringWithLenght(100), "Content"), true };
        yield return new object[] { new CreatePostDto("Title", GetStringWithLenght(5000)), true };
        yield return new object[] { new CreatePostDto("", "Content"), false };
        yield return new object[] { new CreatePostDto(null, "Content"), false };
        yield return new object[] { new CreatePostDto(GetStringWithLenght(101), "Content"), false };
        yield return new object[] { new CreatePostDto("Title", ""), false };
        yield return new object[] { new CreatePostDto("Title", null), false };
        yield return new object[] { new CreatePostDto("Title", GetStringWithLenght(5001)), false };
    }

    [Theory]
    [MemberData(nameof(ValidateGetTestData))]
    public void Validate_ForGivenModel_ReturnsProperValidationResult(CreatePostDto model, bool expectedIsValid)
    {
        var validationResult = _validator.Validate(model);

        foreach (var e in validationResult.Errors)
        {
            _outputHelper.WriteLine(e.ErrorMessage);
        }

        validationResult.IsValid.Should().Be(expectedIsValid);
    }

    private static string GetStringWithLenght(int length)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            sb.Append("x");
        }

        return sb.ToString();
    }
}
