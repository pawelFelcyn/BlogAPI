namespace Domain.Interfaces;

public interface IEmailValidationHelper
{
    bool IsTaken(string email);
}
