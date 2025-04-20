namespace SimRegisPortal.Application.Models.ExchangeRate;

public abstract record ExchangeRateDto
{
    public int FromCurrencyId { get; set; }
    public int ToCurrencyId { get; set; }
    public DateTime Date { get; set; }
    public decimal Rate { get; set; }
}
