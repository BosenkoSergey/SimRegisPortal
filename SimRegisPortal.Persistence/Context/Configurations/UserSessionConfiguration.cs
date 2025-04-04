﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Domain.Entities;
using SimRegisPortal.Persistence.Constants;

namespace SimRegisPortal.Persistence.Context.Configurations
{
    internal class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
    {
        public void Configure(EntityTypeBuilder<UserSession> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasColumnType(EntityFieldPresets.GuidType)
                .HasDefaultValueSql(EntityFieldPresets.DefaultGuid)
                .HasValueGenerator<SequentialGuidValueGenerator>()
                .ValueGeneratedOnAdd();

            builder.Property(s => s.CreatedAt)
                .HasColumnType(EntityFieldPresets.DateTimeType)
                .HasDefaultValueSql(EntityFieldPresets.DefaultDateTime)
                .ValueGeneratedOnAdd();

            builder.Property(s => s.UpdatedAt)
                .HasColumnType(EntityFieldPresets.DateTimeType)
                .HasDefaultValueSql(EntityFieldPresets.DefaultDateTime)
                .ValueGeneratedOnAdd();

            builder.Property(s => s.ExpiresAt)
                .HasColumnType(EntityFieldPresets.DateTimeType)
                .IsRequired();

            builder.HasIndex(s => s.RefreshToken)
                .IsUnique();

            builder.HasOne(s => s.User)
                .WithMany(u => u.Sessions)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(nameof(UserSession));
        }
    }
}
