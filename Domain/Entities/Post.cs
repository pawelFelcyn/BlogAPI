namespace Domain.Entities;

public class Post
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastModyfied { get; set; }
    public int CreatedById { get; set; }
    public virtual User? CreatedBy { get; set; }
}
