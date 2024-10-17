using System;
using System.Collections.Generic;

namespace TaskingSystem.Server.Models;

public partial class TaskStatus
{
    public int Id { get; set; }

    public string NameState { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
