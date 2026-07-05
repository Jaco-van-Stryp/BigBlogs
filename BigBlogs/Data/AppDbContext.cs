using BigBlogs.Entities;
using Microsoft.EntityFrameworkCore;

namespace BigBlogs.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Blogs> Blogs { get; set; }
    public DbSet<Comment> Comment { get; set; }
}
