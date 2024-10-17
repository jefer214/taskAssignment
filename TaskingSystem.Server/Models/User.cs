using System;
using System.Collections.Generic;

namespace TaskingSystem.Server.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RolId { get; set; }
    public virtual Role Rol { get; set; } = null!;
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
