namespace Domain.Exceptions;

public class InvalidPasswordException : BadRequestException
{
    public InvalidPasswordException() : base("Invalid password")
    {
    }
}
