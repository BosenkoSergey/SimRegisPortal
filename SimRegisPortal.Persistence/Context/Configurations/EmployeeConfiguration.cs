using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Constants;

namespace SimRegisPortal.Persistence.Context.Configurations;

internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnType(EntityFieldPresets.GuidType)
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .ValueGeneratedOnAdd();

        builder.Property(e => e.FirstName)
            .HasMaxLength(EntityFieldPresets.DefaultStringLength);

        builder.Property(e => e.MiddleName)
            .HasMaxLength(EntityFieldPresets.DefaultStringLength);

        builder.Property(e => e.LastName)
            .HasMaxLength(EntityFieldPresets.DefaultStringLength);

        builder.Property(e => e.Position)
            .HasMaxLength(EntityFieldPresets.DefaultStringLength);

        builder.Property(e => e.HireDate)
            .HasColumnType(EntityFieldPresets.DateType);

        builder.Property(e => e.DismissalDate)
            .HasColumnType(EntityFieldPresets.DateType);

        builder.Property(e => e.HourlyRate)
            .HasColumnType(EntityFieldPresets.MoneyType);

        builder.HasOne(e => e.User)
            .WithOne(u => u.Employee)
            .HasForeignKey<Employee>(e => e.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.ToTable(nameof(Employee));
    }
}
