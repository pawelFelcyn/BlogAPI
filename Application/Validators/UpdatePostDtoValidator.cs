using Application.Dtos;
using FluentValidation;

namespace Application.Validators;

public class UpdatePostDtoValidator : AbstractValidator<UpdatePostDto>
{
    public UpdatePostDtoValidator()
    {
        RulesForContent();
    }

    private void RulesForContent()
    {
        RuleFor(u => u.Content)
            .NotEmpty()
            .WithMessage("Field 'Content' must not be empty")
            .MaximumLength(5000)
            .WithMessage("The maximum length of the value in field 'Content' is 5000 characters");
    }
}
