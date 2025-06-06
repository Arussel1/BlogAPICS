using Microsoft.EntityFrameworkCore;
using BlogAPI.Models;

namespace BlogAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Post { get; set; }
        
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.AuthorId).HasColumnName("authorId");
                entity.Property(e => e.Content).HasColumnName("content");
                entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
                entity.Property(e => e.Image).HasColumnName("image");
                entity.Property(e => e.Published).HasColumnName("published");
                entity.Property(e => e.Title).HasColumnName("title");
            });
        }
    }
}