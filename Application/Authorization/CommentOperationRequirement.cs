namespace Application.Authorization;

public class CommentOperationRequirement
{
    public CommentOperationType Operation { get; set; }
}

internal enum CommentOperationType
{
    Update, Delete
}
