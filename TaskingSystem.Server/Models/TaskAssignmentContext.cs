using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TaskingSystem.Server.Models;

public partial class TaskAssignmentContext : DbContext
{
    public TaskAssignmentContext()
    {
    }

    public TaskAssignmentContext(DbContextOptions<TaskAssignmentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Task> Tasks { get; set; }
    public virtual DbSet<TaskStatus> TaskStatuses { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Permission> Permissions { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<RoleInPermission> RoleInPermissions { get; set; }
    public virtual DbSet<UsersInTask> UsersInTasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=dataservertaskassignment.database.windows.net;Initial Catalog=taskAssignment;User ID=jefadmin;Password=azusql2024@;Connect Timeout=60;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissi__3214EC0784DD3A2D");

            entity.ToTable("Permission");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.NamePermission).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rol__3214EC07D2C42E42");

            entity.ToTable("Role");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.NameRol).HasMaxLength(50);
        });

        modelBuilder.Entity<RoleInPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoleInPe__3214EC073F458187");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Permission).WithMany(p => p.RoleInPermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleInPer__Permi__7B5B524B");

            entity.HasOne(d => d.Rol).WithMany(p => p.RoleInPermissions)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleInPer__RolId__7A672E12");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07B02FAAF2");

            entity.ToTable("User");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);

            entity.HasOne(d => d.Rol).WithMany(p => p.Users)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__RolId__05D8E0BE");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC076DE93F52");

            entity.ToTable("Task");

            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.NameTask).HasMaxLength(50);

            entity.HasOne(d => d.State).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Task__StateId__2A164134");

            entity.HasOne(d => d.User).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Task__UserId__29221CFB");
        });

        modelBuilder.Entity<TaskStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TaskStat__3214EC07A2722BC5");

            entity.ToTable("TaskStatus");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.NameState).HasMaxLength(50);
        });

        modelBuilder.Entity<UsersInTask>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UsersInTask");

            entity.HasOne(d => d.Task).WithMany()
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserInTas__TaskI__2B0A656D");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserInTas__UserI__151B244E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
