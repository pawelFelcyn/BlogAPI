namespace Domain.Exceptions;

public class CantUpdateCommentException : ForbidException
{
    public CantUpdateCommentException() : base("You can't update this comment")
    {
    }
}
