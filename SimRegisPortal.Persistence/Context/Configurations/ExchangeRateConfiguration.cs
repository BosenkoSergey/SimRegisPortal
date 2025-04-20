using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Constants;

namespace SimRegisPortal.Persistence.Context.Configurations;

internal class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Date)
            .HasColumnType(EntityFieldPresets.DateType);

        builder.Property(x => x.Rate)
            .HasColumnType(EntityFieldPresets.MoneyType);

        builder.HasOne(x => x.FromCurrency)
            .WithMany()
            .HasForeignKey(x => x.FromCurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ToCurrency)
            .WithMany()
            .HasForeignKey(x => x.ToCurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.FromCurrencyId, x.ToCurrencyId, x.Date })
            .IsUnique();

        builder.ToTable(nameof(ExchangeRate));
    }
}
