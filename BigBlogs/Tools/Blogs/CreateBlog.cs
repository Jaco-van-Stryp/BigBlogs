using System.ComponentModel;
using BigBlogs.Data;
using ModelContextProtocol.Server;
using CreateBlogFeature = BigBlogs.Features.Blogs.CreateBlog;

namespace BigBlogs.Tools.Blogs;

[McpServerToolType]
public static class CreateBlog
{
    [McpServerTool, Description("Creates a new blog post.")]
    public static Task<CreateBlogFeature.Response> CreateBlogPostAsync(
        [Description("The blog's title")] string blogTitle,
        [Description("The blog's author")] string blogAuthor,
        [Description("The blog's content")] string blogContent,
        [Description("The blog's category")] string blogCategory,
        AppDbContext context,
        CancellationToken cancellationToken
    ) =>
        CreateBlogFeature.CreateBlogCore(
            blogTitle,
            blogAuthor,
            blogContent,
            blogCategory,
            context,
            cancellationToken
        );
}
