using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Tests.Context;

public static class InMemoryDbContextFactory
{
    public static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);
        context.Database.EnsureCreated();

        SeedDatabase(context);

        return context;
    }

    private static void SeedDatabase(AppDbContext context)
    {
        context.Currencies.AddRange(
            new Currency { Id = 1, Code = "UAH", Name = "Ukrainian Hryvnia", Symbol = "₴" },
            new Currency { Id = 2, Code = "USD", Name = "US Dollar", Symbol = "$" },
            new Currency { Id = 3, Code = "EUR", Name = "Euro", Symbol = "€" }
        );

        context.ExchangeRates.AddRange(
            new ExchangeRate { FromCurrencyId = 2, ToCurrencyId = 1, Rate = 40.00m, Date = DateTime.UtcNow.Date },
            new ExchangeRate { FromCurrencyId = 3, ToCurrencyId = 1, Rate = 43.00m, Date = DateTime.UtcNow.Date }
        );

        context.TaxSettings.Add(new TaxSetting
        {
            LocalCurrencyId = 1,
            MinimumWage = 8000,
            SocialTax = 22,
            Fop2Pit = 20,
            Fop2MilitaryTax = 10m,
            Fop3Pit = 5,
            Fop3MilitaryTax = 1m,
            GigPit = 5m,
            GigMilitaryTax = 5m
        });

        context.SaveChanges();
    }
}