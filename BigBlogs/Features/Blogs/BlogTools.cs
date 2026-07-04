using System.ComponentModel;
using BigBlogs.Data;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.Server;

[McpServerToolType]
public static class BlogTools
{
    public record BlogsResponse(Guid Id, string Title);

    [McpServerTool, Description("Gets all the blog posts.")]
    public static async Task<List<BlogsResponse>> GetBlogs(
        AppDbContext context,
        CancellationToken cancellationToken
    )
    {
        var blogs = await context
            .Blogs.Select(b => new BlogsResponse(b.Id, b.BlogTitle))
            .ToListAsync(cancellationToken);
        return blogs;
    }
}
