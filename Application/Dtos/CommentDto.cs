namespace Application.Dtos;

public record CommentDto
{
    public int Id { get; init; }
    public string? CreatedByNick { get; set; }
    public DateTime LastModyfied { get; set; }
    public string Content { get; set; }
}
