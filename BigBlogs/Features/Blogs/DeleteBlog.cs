using BigBlogs.Data;

namespace BigBlogs.Features.Blogs;

public static class DeleteBlog
{
    public static IEndpointRouteBuilder MapDeleteBlog(this IEndpointRouteBuilder app)
    {
        app.MapDelete("DeleteBlog/{blogId}", Handle).WithTags("DeleteBlog");
        return app;
    }

    private static async Task<IResult> Handle(string blogId, AppDbContext context)
    {
        var blog = await context.Blogs.FindAsync(blogId);
        if (blog is null)
            return TypedResults.NotFound();
        context.Blogs.Remove(blog);
        await context.SaveChangesAsync();
        return TypedResults.Ok();
    }
}
