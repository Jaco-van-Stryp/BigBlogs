using BigBlogs.Data;

namespace BigBlogs.Features.CreateBlog;

public static class CreateBlog
{
    public record Request(
        string BlogTitle,
        string BlogAuthor,
        string BlogContent,
        string BlogCategory
    );

    public record Response(Guid Id);

    public static IEndpointRouteBuilder MapCreateBlog(this IEndpointRouteBuilder app)
    {
        app.MapPost("create-blog", Handle).WithTags("CreateBlog");
        return app;
    }

    internal static async Task<IResult> Handle(
        Request request,
        AppDbContext context,
        CancellationToken cancellationToken
    )
    {
        var blog = new Entities.Blogs
        {
            BlogTitle = request.BlogTitle,
            BlogAuthor = request.BlogAuthor,
            BlogContent = request.BlogContent,
            BlogCategory = request.BlogCategory,
            DatePosted = DateTime.UtcNow,
        };

        context.Blogs.Add(blog);

        await context.SaveChangesAsync(cancellationToken);
        return TypedResults.Created($"get-specific-blog/{blog.Id}", new Response(blog.Id));
    }
}
