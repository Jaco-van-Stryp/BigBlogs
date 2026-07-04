using BigBlogs.Data;
using Microsoft.EntityFrameworkCore;

namespace BigBlogs.Features.Blogs;

public static class UpdateBlog
{
    private record UpdateBlogRequest(Guid Id, string Title, string Content);

    public static IEndpointRouteBuilder MapUpdateBlog(this IEndpointRouteBuilder app)
    {
        app.MapPut("UpdateBlog", Handle).WithTags("UpdateBlog");
        return app;
    }

    private static async Task<IResult> Handle(AppDbContext context, UpdateBlogRequest request)
    {
        var blog = await context.Blogs.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (blog is null)
            return TypedResults.NotFound();
        blog.BlogTitle = request.Title;
        blog.BlogContent = request.Content;
        await context.SaveChangesAsync();
        return TypedResults.Ok();
    }
}
