namespace BigBlogs.Entities;

public class Blogs
{
    public Guid Id { get; set; }
    public required string BlogTitle { get; set; }
    public required string BlogAuthor { get; set; }
    public required string BlogContent { get; set; }
    public required string BlogCategory { get; set; }
    public DateTime DatePosted { get; set; } = DateTime.UtcNow;
}
