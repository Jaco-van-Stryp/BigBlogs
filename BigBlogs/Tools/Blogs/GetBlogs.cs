using System.ComponentModel;
using BigBlogs.Data;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.Server;

namespace BigBlogs.Tools.Blogs;

[McpServerToolType]
public static class GetBlogs
{
    public record BlogSummary(Guid Id, string Title);

    [McpServerTool, Description("Gets all the blog posts.")]
    public static async Task<List<BlogSummary>> GetBlogsAsync(
        AppDbContext context,
        CancellationToken cancellationToken
    )
    {
        return await context
            .Blogs.Select(b => new BlogSummary(b.Id, b.BlogTitle))
            .ToListAsync(cancellationToken);
    }
}
