namespace Application.Dtos;

public record PostDetailsDto
{
    public int Id { get; set; }
    public string? Title { get; init; }
    public string? Content { get; init; }
    public DateTime LastModyfied { get; init; }
    public DateTime Created { get; init; }
}
