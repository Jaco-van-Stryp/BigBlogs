namespace BigBlogs.Features.Blogs;

public static class BlogsEndpoints
{
    public static IEndpointRouteBuilder MapBlogEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("blogs").WithTags("Blogs");
        group.MapCreateBlog();
        group.MapGetAllBlogs();
        group.MapGetSpecificBlog();
        group.MapDeleteBlog();
        group.MapUpdateBlog();

        return group;
    }
}
