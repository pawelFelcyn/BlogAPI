namespace Domain.Exceptions;

public class CommentNotFoundException : NotFoundException
{
    public CommentNotFoundException() : base("Comment with given id was not found in database")
    {
    }
}
