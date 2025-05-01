namespace SimRegisPortal.Application.Models.Entities.Related;

public sealed class ExchangeRatesImportRequest
{
    public DateTime? ImportDate { get; set; } = DateTime.UtcNow.Date;
}
