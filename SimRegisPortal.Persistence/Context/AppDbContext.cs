using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Helpers;

namespace SimRegisPortal.Persistence.Context;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserPermission> UserPermissions { get; set; } = null!;
    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<Contract> Contracts { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<EmployeeActivity> EmployeeActivities { get; set; } = null!;
    public DbSet<Currency> Currencies { get; set; } = null!;
    public DbSet<ExchangeRate> ExchangeRates { get; set; } = null!;
    public DbSet<TimeReport> TimeReports { get; set; } = null!;
    public DbSet<PaymentRequest> PaymentRequests { get; set; } = null!;
    public DbSet<TaxSetting> TaxSettings { get; set; } = null!;
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

        if (!Projects.Any())
        {
            var defaultCompany = Companies.FirstOrDefault(c => c.Name == "Default Company");
            if (defaultCompany == null)
            {
                defaultCompany = new Company
                {
                    Name = "Default Company",
                    Notes = "Default company for system projects."
                };

                Companies.Add(defaultCompany);
                SaveChanges();
            }

            var systemProjects = new[]
            {
                new Project { CompanyId = defaultCompany.Id, Name = "Paid Leave", Description = "Vacation time tracking", StartDate = DateTime.UtcNow },
                new Project { CompanyId = defaultCompany.Id, Name = "Sick Leave", Description = "Sick leave tracking", StartDate = DateTime.UtcNow }
            };

            Projects.AddRange(systemProjects);
            SaveChanges();
        }

        if (!Currencies.Any())
        {
            var currencies = new[]
            {
                new Currency { Code = "UAH", Name = "Ukrainian Hryvnia", Symbol = "₴" },
                new Currency { Code = "USD", Name = "US Dollar", Symbol = "$" },
                new Currency { Code = "EUR", Name = "Euro", Symbol = "€" },
                new Currency { Code = "GBP", Name = "British Pound", Symbol = "£" },
                new Currency { Code = "PLN", Name = "Polish Zloty", Symbol = "zł" },
                new Currency { Code = "CHF", Name = "Swiss Franc", Symbol = "Fr" }
            };

            Currencies.AddRange(currencies);
            SaveChanges();
        }

        if (!TaxSettings.Any())
        {
            var localCurrencyId = Currencies.First(c => c.Code == "UAH").Id;
            var taxSetting = new TaxSetting
            {
                LocalCurrencyId = localCurrencyId,
                MinimumWage = 8000,
                SocialTax = 22,
                Fop2Pit = 20,
                Fop2MilitaryTax = 10,
                Fop3Pit = 5,
                Fop3MilitaryTax = 1,
                GigPit = 5,
                GigMilitaryTax = 5
            };

            TaxSettings.Add(taxSetting);
            SaveChanges();
        }
    }
}
