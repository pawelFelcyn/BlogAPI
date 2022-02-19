namespace Domain.Exceptions;

public class PostNotFoundException : NotFoundException
{
    public PostNotFoundException() : base("Post with given id was not found in database")
    {
    }
}
