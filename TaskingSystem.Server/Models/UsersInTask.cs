using System;
using System.Collections.Generic;

namespace TaskingSystem.Server.Models;

public partial class UsersInTask
{
    public int UserId { get; set; }

    public int TaskId { get; set; }

    public virtual Task Task { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
