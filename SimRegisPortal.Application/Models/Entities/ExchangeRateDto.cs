namespace SimRegisPortal.Application.Models.Entities;

public sealed class ExchangeRateDto : BaseEntityDto<int>
{
    public int FromCurrencyId { get; set; }
    public int ToCurrencyId { get; set; }
    public DateTime Date { get; set; }
    public decimal Rate { get; set; }
}
