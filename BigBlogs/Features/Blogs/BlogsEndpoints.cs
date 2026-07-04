using BigBlogs.Features.Blogs.GetAllBlogs;
using BigBlogs.Features.Blogs.GetSpecificBlog;
using BigBlogs.Features.CreateBlog;

namespace BigBlogs.Features.Blogs;

public static class BlogsEndpoints
{
    public static IEndpointRouteBuilder MapBlogEndpoints(this IEndpointRouteBuilder group)
    {
        group.MapGroup("blogs").WithTags("Blogs");
        group.MapCreateBlog();
        group.MapGetAllBlogs();
        group.MapGetSpecificBlog();
        return group;
    }
}
