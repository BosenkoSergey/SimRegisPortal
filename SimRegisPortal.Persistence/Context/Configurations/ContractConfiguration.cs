using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Constants;

namespace SimRegisPortal.Persistence.Context.Configurations;

internal sealed class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnType(EntityFieldPresets.GuidType)
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Number)
            .HasMaxLength(EntityFieldPresets.DefaultStringLength);

        builder.Property(e => e.Notes)
            .HasMaxLength(EntityFieldPresets.DefaultStringLength);

        builder.Property(p => p.DateFrom)
            .HasColumnType(EntityFieldPresets.DateType);

        builder.Property(p => p.DateTo)
            .HasColumnType(EntityFieldPresets.DateType);

        builder.HasOne(p => p.Project)
            .WithMany(p => p.Contracts)
            .HasForeignKey(p => p.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Employee)
            .WithMany(u => u.Contracts)
            .HasForeignKey(p => p.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(nameof(Contract));
    }
}
