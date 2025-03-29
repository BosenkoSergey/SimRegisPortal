using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Domain.Entities;
using SimRegisPortal.Persistence.Constants;

namespace SimRegisPortal.Persistence.Context.Configurations
{
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
                .HasMaxLength(EntityFieldPresets.DefaultStringLength)
                .IsRequired();

            builder.Property(l => l.Message)
                .IsRequired();

            builder.Property(l => l.MessageTemplate);

            builder.Property(l => l.Exception);

            builder.Property(l => l.Properties);

            builder.ToTable(nameof(SystemLog));
        }
    }
}
