using BigBlogs.Data;
using BigBlogs.Features;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

//Configure Postgres Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder
    .Services.AddMcpServer()
    .WithHttpTransport(options => options.Stateless = true)
    .WithToolsFromAssembly();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "McpInspector",
        policy =>
            policy
                .AllowAnyOrigin()
                .WithMethods("POST", "GET", "DELETE")
                .WithHeaders("Content-Type", "Authorization", "MCP-Protocol-Version", "Mcp-Session-Id")
                .WithExposedHeaders("Mcp-Session-Id")
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseCors("McpInspector");
}

app.MapEndpoints();
app.MapMcp();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.Run();
