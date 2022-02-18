namespace Application.Dtos;

public record PostDto
{
    public int Id { get; init; }
    public string? Title { get; init; }
    public DateTime LastModyfied { get; init; }
}
