using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace BlogAPI.Models;
[Table("Post")]
public partial class Post
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Content { get; set; } = null!;

    public int AuthorId { get; set; }

    public bool Published { get; set; }

    public string Image { get; set; } = null!;

    public virtual User Author { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
