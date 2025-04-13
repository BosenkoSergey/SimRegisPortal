using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Constants;

namespace SimRegisPortal.Persistence.Context.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnType(EntityFieldPresets.GuidType)
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .ValueGeneratedOnAdd();

        builder.Property(u => u.FullName)
            .HasMaxLength(EntityFieldPresets.DefaultStringLength);

        builder.Property(u => u.Email)
            .HasMaxLength(EntityFieldPresets.DefaultStringLength);

        builder.Property(u => u.Login)
            .HasMaxLength(EntityFieldPresets.DefaultStringLength);

        builder.Property(u => u.PasswordHash)
            .HasMaxLength(EntityFieldPresets.DefaultStringLength);

        builder.ToTable(nameof(User));
    }
}
