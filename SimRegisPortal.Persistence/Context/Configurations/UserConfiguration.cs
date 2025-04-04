﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Domain.Entities;
using SimRegisPortal.Persistence.Constants;

namespace SimRegisPortal.Persistence.Context.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnType(EntityFieldPresets.GuidType)
                .HasDefaultValueSql(EntityFieldPresets.DefaultGuid)
                .HasValueGenerator<SequentialGuidValueGenerator>()
                .ValueGeneratedOnAdd();

            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(EntityFieldPresets.DefaultStringLength);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(EntityFieldPresets.DefaultStringLength);

            builder.Property(u => u.Login)
                .IsRequired()
                .HasMaxLength(EntityFieldPresets.DefaultStringLength);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(EntityFieldPresets.DefaultStringLength);

            builder.ToTable(nameof(User));
        }
    }
}
