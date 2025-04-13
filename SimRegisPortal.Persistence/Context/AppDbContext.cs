using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Helpers;

namespace SimRegisPortal.Persistence.Context;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserSession> UserSessions { get; set; } = null!;
    public DbSet<UserPermission> UserPermissions { get; set; } = null!;
    public DbSet<UserProjectPermission> UserProjectPermissions { get; set; } = null!;
    public DbSet<SystemLog> SystemLogs { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public void Seed()
    {
        if (!Users.Any())
        {
            var admin = new User
            {
                FullName = "Administrator",
                Email = "admin@srp.local",
                Login = "Admin",
                PasswordHash = PasswordHelper.GetHash("Admin123"),
                IsAdmin = true
            };

            Users.Add(admin);
            SaveChanges();
        }
    }
}
