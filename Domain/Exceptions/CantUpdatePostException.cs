namespace Domain.Exceptions;

public class CantUpdatePostException : ForbidException
{
    public CantUpdatePostException() : base("You cant update this post")
    {
    }
}
