using Application.Dtos;
using FluentValidation;

namespace Application.Validators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RulesForEmail();
        RulesForPassword();
    }

    private void RulesForEmail()
    {
        RuleFor(l => l.Email)
            .NotEmpty()
            .WithMessage("Field 'Email' must not be empty");
    }

    private void RulesForPassword()
    {
        RuleFor(l => l.Password)
            .NotEmpty()
            .WithMessage("Fiels 'Password' must not be empty");
    }
}
