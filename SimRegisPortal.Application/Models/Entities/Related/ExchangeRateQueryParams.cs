namespace SimRegisPortal.Application.Models.Entities.Related;

public sealed class ExchangeRateQueryParams
{
    public int? FromCurrencyId { get; set; }
    public int? ToCurrencyId { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
}
