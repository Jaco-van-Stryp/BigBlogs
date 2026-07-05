namespace BigBlogs.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public required string Content { get; set; }
    public required string Author { get; set; }
    public DateTime DatePosted { get; set; } = DateTime.UtcNow;

    public Blogs Blog { get; set; } = null!;
    public Guid BlogId { get; set; }
}
