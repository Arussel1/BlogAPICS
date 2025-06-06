using BlogAPI.Data;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Blog API",
        Version = "v1",
        Description = "An API for managing blog posts",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Your Name",
            Email = "your.email@example.com"
        }
    });
});


// Register AppDbContext
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connStr));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/posts/{id}", async (int id, AppDbContext db) =>
{
    var post = await db.Post.FindAsync(id);
    if (post is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(post);
})
.WithName("GetPostById")
.WithOpenApi()
.Produces<Post>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound)
.WithTags("Posts")
.WithSummary("Get a specific post by ID")
.WithDescription("Retrieves a blog post based on its unique identifier");

app.MapPatch("/posts/{id}", async (int id, Post post, AppDbContext db) =>
{
    var existingPost = await db.Post.FindAsync(id);
    if (existingPost is null) return Results.NotFound();

    existingPost.Published = post.Published;

    await db.SaveChangesAsync();

    return Results.NoContent();
}).WithName("ChangePostStatus")
.WithOpenApi()
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.WithTags("Posts")
.WithSummary("Change the published status of a post")
.WithDescription("Updates the published status of a blog post by its ID");

app.MapGet("/posts", async (AppDbContext db) =>
{
    var posts = await db.Post
        .Where(p => p.Published)
        .ToListAsync();
    return Results.Ok(posts);
})
.WithName("GetPosts")
.WithOpenApi()
.Produces<List<Post>>(StatusCodes.Status200OK)
.WithTags("Posts")
.WithSummary("Get all published posts")
.WithDescription("Retrieves all published blog posts");




app.Run();