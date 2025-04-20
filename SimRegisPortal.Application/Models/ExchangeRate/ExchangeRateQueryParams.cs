namespace SimRegisPortal.Application.Models.ExchangeRate;

public sealed record ExchangeRateQueryParams
{
    public int? FromCurrencyId { get; set; }
    public int? ToCurrencyId { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
}
