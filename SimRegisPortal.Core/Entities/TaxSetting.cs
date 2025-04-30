namespace SimRegisPortal.Core.Entities;

public sealed class TaxSetting : BaseEntity<int>
{
    public int LocalCurrencyId { get; set; }
    public decimal MinimumWage { get; set; }
    public decimal SocialTax { get; set; }
    public decimal Fop2Pit { get; set; }
    public decimal Fop2MilitaryTax { get; set; }
    public decimal Fop3Pit { get; set; }
    public decimal Fop3MilitaryTax { get; set; }
    public decimal GigPit { get; set; }
    public decimal GigMilitaryTax { get; set; }

    public Currency LocalCurrency { get; set; } = default!;
}