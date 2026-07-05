using System.ComponentModel;
using BigBlogs.Data;
using BigBlogs.Features.Blogs;
using ModelContextProtocol.Server;

namespace BigBlogs.Tools.Blogs;

[McpServerToolType]
public class GetSpecificBlogTool(AppDbContext context)
{
    [McpServerTool, Description("Get a Specific Blog by ID to view it's contents and comments")]
    public async Task<Features.Blogs.GetSpecificBlog.GetSpecificBlogResponse> GetSpecificBlogById(
        [Description("The ID of the blog to get")] Guid blogId
    )
    {
        var blog = await GetSpecificBlog.GetSpecificBlogCore(blogId, context);
        return blog ?? throw new Exception("Blog not found");
    }
}
