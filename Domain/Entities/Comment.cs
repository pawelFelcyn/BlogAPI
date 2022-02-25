namespace Domain.Entities;

public class Comment
{
    public int Id { get; set; }
    public string? Content { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastModyfied { get; set; }
    public int CreatedById { get; set; }
    public User? CreatedBy { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
}
