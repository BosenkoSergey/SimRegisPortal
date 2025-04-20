using SimRegisPortal.Application.Models.Banking;

namespace SimRegisPortal.Application.Services.Interfaces;

public interface IPrivatBankService
{
    Task<IEnumerable<PrivatBankRateDto>> GetRatesAsync(DateTime date, CancellationToken cancellationToken = default);
}
