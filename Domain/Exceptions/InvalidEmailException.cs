namespace Domain.Exceptions;

public class InvalidEmailException : BadRequestException
{
    public InvalidEmailException() : base("Invalid email")
    {
    }
}
