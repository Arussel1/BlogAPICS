﻿using System;
using System.Collections.Generic;

namespace BlogAPI.Models;

public partial class Comment
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int PostId { get; set; }

    public int AuthorId { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}
