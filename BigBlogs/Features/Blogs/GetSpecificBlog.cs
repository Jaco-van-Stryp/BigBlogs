using BigBlogs.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BigBlogs.Features.Blogs.GetSpecificBlog;

public static class GetSpecificBlog
{
    private readonly record struct GetSpecificBlogResponse(
        Guid Id,
        string BlogTitle,
        string BlogAuthor,
        string BlogContent,
        string BlogCategory,
        DateTime DatePosted
    );

    public static IEndpointRouteBuilder MapGetSpecificBlog(this IEndpointRouteBuilder app)
    {
        app.MapGet("get-specific-blog/{blogId}", HandleAsync).WithTags("GetSpecificBlog");
        return app;
    }

    internal static async Task<IResult> HandleAsync(Guid blogId, AppDbContext context)
    {
        var blogs = await context.Blogs.FirstOrDefaultAsync(x => x.Id == blogId);
        if (blogs == null)
            return Results.NotFound();
        var response = new GetSpecificBlogResponse(
            blogs.Id,
            blogs.BlogTitle,
            blogs.BlogAuthor,
            blogs.BlogContent,
            blogs.BlogCategory,
            blogs.DatePosted
        );
        return TypedResults.Ok(blogs);
    }
}
