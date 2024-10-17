namespace TaskingSystem.Server.Models;

public partial class Task
{
    public int Id { get; set; }

    public string NameTask { get; set; } = null!;

    public string? Description { get; set; }

    public int UserId { get; set; }

    public int StateId { get; set; }

    public virtual TaskStatus State { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
