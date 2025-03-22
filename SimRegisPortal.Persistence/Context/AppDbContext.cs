using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Domain.Entities;

namespace SimRegisPortal.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; } = null!;

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
            if (!UserAccounts.Any())
            {
                var admin = new UserAccount
                {
                    FullName = "Administrator",
                    Email = "admin@simregis.local",
                    Login = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    IsAdmin = true
                };

                UserAccounts.Add(admin);
                SaveChanges();
            }
        }
    }
}
