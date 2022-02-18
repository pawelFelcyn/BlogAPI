namespace Application.Dtos;

public record PostDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public DateTime LastModyfied { get; set; }
}
