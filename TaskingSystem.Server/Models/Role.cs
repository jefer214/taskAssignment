namespace TaskingSystem.Server.Models;

public partial class Role
{
    public int Id { get; set; }

    public string NameRol { get; set; } = null!;
    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual ICollection<RoleInPermission> RoleInPermissions { get; set; } = new List<RoleInPermission>();
}
