using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Constants;

namespace SimRegisPortal.Persistence.Context.Configurations;

internal class EmployeeActivityConfiguration : IEntityTypeConfiguration<EmployeeActivity>
{
    public void Configure(EntityTypeBuilder<EmployeeActivity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType(EntityFieldPresets.GuidType)
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Date)
            .HasColumnType(EntityFieldPresets.DateType);

        builder.Property(x => x.Hours)
            .HasColumnType("decimal(5,2)");

        builder.Property(x => x.Description)
            .HasMaxLength(EntityFieldPresets.DefaultStringLength);

        builder.HasOne(x => x.Employee)
            .WithMany(e => e.Activities)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Project)
            .WithMany()
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(nameof(EmployeeActivity));
    }
}