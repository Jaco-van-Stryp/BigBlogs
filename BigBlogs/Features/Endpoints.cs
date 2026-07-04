using BigBlogs.Features.Blogs;

namespace BigBlogs.Features;

public static class Endpoints
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api").WithTags("BigBlogs");
        group.MapBlogEndpoints();
        return group;
    }
}
