using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Constants;

namespace SimRegisPortal.Persistence.Context.Configurations;

internal class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Code)
            .HasMaxLength(3);

        builder.Property(c => c.Name)
            .HasMaxLength(EntityFieldPresets.DefaultStringLength);

        builder.Property(c => c.Symbol)
            .HasMaxLength(5);

        builder.HasIndex(c => c.Code)
            .IsUnique();

        builder.ToTable(nameof(Currency));
    }
}