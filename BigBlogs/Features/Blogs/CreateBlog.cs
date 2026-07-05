using BigBlogs.Data;

namespace BigBlogs.Features.Blogs;

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
        var response = await CreateBlogCore(
            request.BlogTitle,
            request.BlogAuthor,
            request.BlogContent,
            request.BlogCategory,
            context,
            cancellationToken
        );
        return TypedResults.Created($"get-specific-blog/{response.Id}", response);
    }

    internal static async Task<Response> CreateBlogCore(
        string blogTitle,
        string blogAuthor,
        string blogContent,
        string blogCategory,
        AppDbContext context,
        CancellationToken cancellationToken
    )
    {
        var blog = new Entities.Blogs
        {
            BlogTitle = blogTitle,
            BlogAuthor = blogAuthor,
            BlogContent = blogContent,
            BlogCategory = blogCategory,
            DatePosted = DateTime.UtcNow,
        };

        context.Blogs.Add(blog);

        await context.SaveChangesAsync(cancellationToken);
        return new Response(blog.Id);
    }
}
