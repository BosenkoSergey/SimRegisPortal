namespace SimRegisPortal.Core.Entities;

public sealed class ExchangeRate : BaseEntity<int>
{
    public int FromCurrencyId { get; set; }
    public int ToCurrencyId { get; set; }
    public DateTime Date { get; set; }
    public decimal Rate { get; set; }

    public Currency FromCurrency { get; set; } = default!;
    public Currency ToCurrency { get; set; } = default!;
}