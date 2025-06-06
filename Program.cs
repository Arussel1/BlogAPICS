using BlogAPI.Data;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
.WithTags("Posts");

app.Run();