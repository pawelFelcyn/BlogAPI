using Application.Dtos;
using FluentValidation;

namespace Application.Validators;

public class CreatePostDtoValidator : AbstractValidator<CreatePostDto>
{
    public CreatePostDtoValidator()
    {
        RulesForTitle();
        RulesForContent();
    }

    private void RulesForTitle()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .WithMessage("Field 'Title' must not be empty")
            .MaximumLength(100)
            .WithMessage("The maximum length of the value in field 'Title' is 100 characters");
    }

    private void RulesForContent()
    {
        RuleFor(c => c.Content)
            .NotEmpty()
            .WithMessage("Field 'Content' must not be empty")
            .MaximumLength(5000)
            .WithMessage("The maximum length of the value in field 'Content' is 5000 characters");
    }
}
