using System.ComponentModel;
using BigBlogs.Data;
using ModelContextProtocol.Server;
using CreateCommentFeature = BigBlogs.Features.Comments.CreateComment;

namespace BigBlogs.Tools.Comments;

[McpServerToolType]
public static class CreateComment
{
    [McpServerTool, Description("Adds a comment to an existing blog post.")]
    public static async Task<CreateCommentFeature.Response> CreateCommentAsync(
        [Description("The Id of the blog to comment on")] Guid blogId,
        [Description("The comment content to be added")] string comment,
        [Description("The Author of the comment")] string author,
        AppDbContext context,
        CancellationToken cancellationToken
    )
    {
        var response = await CreateCommentFeature.CreateCommentCore(
            blogId,
            comment,
            author,
            context,
            cancellationToken
        );
        if (response is null)
            throw new Exception("Blog not found");

        return response;
    }
}
