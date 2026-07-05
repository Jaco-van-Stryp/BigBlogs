using BigBlogs.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BigBlogs.Features.Blogs;

public static class GetSpecificBlog
{
    public record GetSpecificBlogResponse(
        Guid Id,
        string BlogTitle,
        string BlogAuthor,
        string BlogContent,
        string BlogCategory,
        DateTime DatePosted,
        IEnumerable<GetComment> Comments
    );

    public record GetComment(
        Guid Id,
        string CommentAuthor,
        string CommentContent,
        DateTime DatePosted
    );

    public static IEndpointRouteBuilder MapGetSpecificBlog(this IEndpointRouteBuilder app)
    {
        app.MapGet("get-specific-blog/{blogId}", HandleAsync).WithTags("GetSpecificBlog");
        return app;
    }

    public static async Task<IResult> HandleAsync(Guid blogId, AppDbContext context)
    {
        var blogs = await GetSpecificBlogCore(blogId, context);
        if (blogs == null)
            return Results.NotFound();
        return TypedResults.Ok(blogs);
    }

    public static async Task<GetSpecificBlogResponse?> GetSpecificBlogCore(
        Guid blogId,
        AppDbContext context
    )
    {
        var blogs = await context
            .Blogs.Include(x => x.Comments)
            .FirstOrDefaultAsync(x => x.Id == blogId);
        if (blogs == null)
            return null;

        var comments = blogs.Comments.Select(x => new GetComment(
            x.Id,
            x.Author,
            x.Content,
            x.DatePosted
        ));

        var response = new GetSpecificBlogResponse(
            blogs.Id,
            blogs.BlogTitle,
            blogs.BlogAuthor,
            blogs.BlogContent,
            blogs.BlogCategory,
            blogs.DatePosted,
            comments
        );

        return response;
    }
}
