using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Constants;

namespace SimRegisPortal.Persistence.Context.Configurations;

internal class SystemLogConfiguration : IEntityTypeConfiguration<SystemLog>
{
    public void Configure(EntityTypeBuilder<SystemLog> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Id)
            .ValueGeneratedOnAdd();

        builder.Property(l => l.TimeStamp)
            .HasColumnType(EntityFieldPresets.DateTimeType)
            .HasDefaultValueSql(EntityFieldPresets.DefaultDateTime);

        builder.Property(l => l.Level)
            .HasMaxLength(EntityFieldPresets.DefaultStringLength);

        builder.ToTable(nameof(SystemLog));
    }
}
