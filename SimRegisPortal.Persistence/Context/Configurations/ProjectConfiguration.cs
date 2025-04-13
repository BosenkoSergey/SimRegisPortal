using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Constants;

namespace SimRegisPortal.Persistence.Context.Configurations;

internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnType(EntityFieldPresets.GuidType)
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .ValueGeneratedOnAdd();

        builder.ToTable(nameof(Project));
    }
}
