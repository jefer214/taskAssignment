using System;
using System.Collections.Generic;

namespace TaskingSystem.Server.Models;

public partial class Permission
{
    public int Id { get; set; }

    public string NamePermission { get; set; } = null!;

    public virtual ICollection<RoleInPermission> RoleInPermissions { get; set; } = new List<RoleInPermission>();
}
