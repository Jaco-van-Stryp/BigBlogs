using BigBlogs.Data;
using Microsoft.EntityFrameworkCore;

namespace BigBlogs.Features.Blogs.GetAllBlogs;

public static class GetAllBlogs
{
    public record GetAllBlogsResponse(
        Guid Id,
        string BlogTitle,
        string BlogAuthor,
        string BlogCategory,
        DateTime DatePosted
    );

    public static IEndpointRouteBuilder MapGetAllBlogs(this IEndpointRouteBuilder app)
    {
        app.MapGet("get-all-blogs", HandleRequest).WithTags("GetAllBlogs");
        return app;
    }

    internal static async Task<IResult> HandleRequest(
        AppDbContext context,
        CancellationToken cancellationToken
    )
    {
        var blogs = await context
            .Blogs.Select(blog => new
            {
                blog.Id,
                blog.BlogTitle,
                blog.BlogContent,
                blog.BlogCategory,
                blog.DatePosted,
            })
            .ToListAsync(cancellationToken);

        var response = blogs
            .Select(blog => new GetAllBlogsResponse(
                blog.Id,
                blog.BlogTitle,
                blog.BlogContent,
                blog.BlogCategory,
                blog.DatePosted
            ))
            .ToList();

        return TypedResults.Ok(response);
    }
}
