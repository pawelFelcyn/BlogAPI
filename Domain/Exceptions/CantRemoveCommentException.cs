namespace Domain.Exceptions;

public class CantRemoveCommentException : ForbidException
{
    public CantRemoveCommentException() : base("You can't remove this comment")
    {
    }
}
