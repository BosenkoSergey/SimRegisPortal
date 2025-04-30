using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Constants;

namespace SimRegisPortal.Persistence.Context.Configurations;

internal class TaxSettingConfiguration : IEntityTypeConfiguration<TaxSetting>
{
    public void Configure(EntityTypeBuilder<TaxSetting> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.MinimumWage)
            .HasColumnType(EntityFieldPresets.MoneyType);

        builder.Property(x => x.SocialTax)
            .HasColumnType(EntityFieldPresets.RateType);

        builder.Property(x => x.Fop2Pit)
            .HasColumnType(EntityFieldPresets.RateType);

        builder.Property(x => x.Fop2MilitaryTax)
            .HasColumnType(EntityFieldPresets.RateType);

        builder.Property(x => x.Fop3Pit)
            .HasColumnType(EntityFieldPresets.RateType);

        builder.Property(x => x.Fop3MilitaryTax)
            .HasColumnType(EntityFieldPresets.RateType);

        builder.Property(x => x.GigPit)
            .HasColumnType(EntityFieldPresets.RateType);

        builder.Property(x => x.GigMilitaryTax)
            .HasColumnType(EntityFieldPresets.RateType);

        builder.HasOne(x => x.LocalCurrency)
            .WithMany()
            .HasForeignKey(x => x.LocalCurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(nameof(TaxSetting));
    }
}