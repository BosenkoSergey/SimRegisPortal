using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Constants;

namespace SimRegisPortal.Persistence.Context.Configurations;

internal class TimeReportConfiguration : IEntityTypeConfiguration<TimeReport>
{
    public void Configure(EntityTypeBuilder<TimeReport> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType(EntityFieldPresets.GuidType)
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Year)
            .IsRequired();

        builder.Property(x => x.Month)
            .IsRequired();

        builder.HasIndex(x => new { x.EmployeeId, x.Year, x.Month })
            .IsUnique();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnType(EntityFieldPresets.DateTimeType)
            .HasDefaultValueSql(EntityFieldPresets.DefaultDateTime)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UpdatedAt)
            .HasColumnType(EntityFieldPresets.DateTimeType)
            .HasDefaultValueSql(EntityFieldPresets.DefaultDateTime)
            .ValueGeneratedOnAddOrUpdate();

        builder.HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(nameof(TimeReport));
    }
}
