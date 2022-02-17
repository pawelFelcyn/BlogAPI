using Application.Dtos;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Validators;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    private readonly IEmailValidationHelper _emailValidationHelper;
    private readonly string[] _allowedRoles = new[] { "User", "Admin" };
    private const string SEPARATOR = ", ";

    public RegisterDtoValidator(IEmailValidationHelper emailValidationHelper)
    {
        _emailValidationHelper = emailValidationHelper;

        RulesForFirstName();
        RulesForLastName();
        RulesForNick();
        RulesForRole();
        RulesForEmail();
        RulesForPassword();
        RulesForConfirmPassword();
    }

    private void RulesForFirstName()
    {
        RuleFor(r => r.FirstName)
            .NotEmpty()
            .WithMessage("Field 'FirstName' must not be empty")
            .MaximumLength(20)
            .WithMessage("The maximum length of the value of field 'FirstName' is 20 characters");
    }

    private void RulesForLastName()
    {
        RuleFor(r => r.LastName)
            .NotEmpty()
            .WithMessage("Field 'LastName' must not be empty")
            .MaximumLength(20)
            .WithMessage("The maximum length of the value of field 'LastName' is 20 characters");
    }

    private void RulesForNick()
    {
        RuleFor(r => r.Nick)
            .NotEmpty()
            .WithMessage("Field 'Nick' must not be empty")
            .MaximumLength(30)
            .WithMessage("The maximum length of the value of field 'Nick' is 30 characters");
    }

    private void RulesForRole()
    {
        RuleFor(r => r.Role)
            .NotEmpty()
            .WithMessage("Field 'Role' must not be empty")
            .Must(r => _allowedRoles.Contains(r))
            .WithMessage($"The value of filed 'Role' must be in {string.Join(SEPARATOR, _allowedRoles)}");
    }

    private void RulesForEmail()
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .WithMessage("Field 'Email' must not be empty")
            .EmailAddress()
            .WithMessage("This is not a valid email address")
            .Must(e => !_emailValidationHelper.IsTaken(e))
            .WithMessage("That email is taken");
    }

    private void RulesForPassword()
    {
        RuleFor(r => r.Password)
            .NotEmpty()
            .WithMessage("Field 'Password' must not be empty")
            .MinimumLength(8)
            .WithMessage("Length of the value of field 'Password' must be at least 8 characters");
    }

    private void RulesForConfirmPassword()
    {
        RuleFor(r => r.ConfirmPassword)
            .NotEmpty()
            .WithMessage("Field 'ConfirmPassword' must not be empty")
            .Equal(r => r.Password)
            .WithMessage("The value of 'ConfirmPassword' must be equal to the value of 'Password'");
    }
}
