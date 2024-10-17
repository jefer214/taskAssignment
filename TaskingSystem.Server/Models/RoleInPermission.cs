using System;
using System.Collections.Generic;

namespace TaskingSystem.Server.Models;

public partial class RoleInPermission
{
    public int Id { get; set; }

    public int RolId { get; set; }

    public int PermissionId { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual Role Rol { get; set; } = null!;
}
