namespace Domain.Exceptions;

public class CantDeletePostException : ForbidException
{
    public CantDeletePostException() : base("You cant delete this post")
    {
    }
}
