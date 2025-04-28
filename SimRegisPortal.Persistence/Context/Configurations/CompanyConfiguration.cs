using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Constants;

namespace SimRegisPortal.Persistence.Context.Configurations;

internal class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnType(EntityFieldPresets.GuidType)
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Name)
            .HasMaxLength(EntityFieldPresets.DefaultStringLength)
            .IsRequired();

        builder.Property(c => c.TaxNumber)
            .HasMaxLength(EntityFieldPresets.DefaultStringLength);

        builder.Property(c => c.RegistrationNumber)
            .HasMaxLength(EntityFieldPresets.DefaultStringLength);

        builder.Property(c => c.Address)
            .HasMaxLength(EntityFieldPresets.DefaultStringLength);

        builder.Property(c => c.Phone)
            .HasMaxLength(EntityFieldPresets.DefaultStringLength);

        builder.Property(c => c.Email)
            .HasMaxLength(EntityFieldPresets.DefaultStringLength);

        builder.Property(c => c.ContactPerson)
            .HasMaxLength(EntityFieldPresets.DefaultStringLength);

        builder.Property(c => c.Notes)
            .HasMaxLength(EntityFieldPresets.DefaultStringLength);

        builder.ToTable(nameof(Company));
    }
}