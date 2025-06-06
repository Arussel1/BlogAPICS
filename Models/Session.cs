using System;
using System.Collections.Generic;

namespace BlogAPI.Models;

public partial class Session
{
    public string Id { get; set; } = null!;

    public string Sid { get; set; } = null!;

    public string Data { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }
}
