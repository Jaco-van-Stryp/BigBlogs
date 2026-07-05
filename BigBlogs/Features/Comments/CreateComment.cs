using BigBlogs.Data;
using BigBlogs.Entities;
using Microsoft.EntityFrameworkCore;

namespace BigBlogs.Features.Comments;

public static class CreateComment
{
    public record Request(Guid BlogId, string CommentContent, string CommentAuthor);

    public record Response(Guid Id);

    public static IEndpointRouteBuilder MapCreateComment(this IEndpointRouteBuilder app)
    {
        app.Map("create-comment", Handle).WithTags("CreateComment");
        return app;
    }

    public static async Task<IResult> Handle(
        AppDbContext context,
        Request request,
        CancellationToken cancellationToken
    )
    {
        var response = await CreateCommentCore(
            request.BlogId,
            request.CommentContent,
            request.CommentAuthor,
            context,
            cancellationToken
        );
        if (response is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(response);
    }

    internal static async Task<Response?> CreateCommentCore(
        Guid blogId,
        string commentContent,
        string commentAuthor,
        AppDbContext context,
        CancellationToken cancellationToken
    )
    {
        var blogExists = await context.Blogs.AnyAsync(x => x.Id == blogId, cancellationToken);
        if (!blogExists)
            return null;

        var comment = new Comment
        {
            Author = commentAuthor,
            Content = commentContent,
            BlogId = blogId,
        };

        context.Comment.Add(comment);

        await context.SaveChangesAsync(cancellationToken);
        return new Response(comment.Id);
    }
}
