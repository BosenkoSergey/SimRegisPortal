namespace SimRegisPortal.Application.Services.Interfaces;

public interface ICurrencyConverter
{
    Task<decimal> ConvertAsync(decimal amount, int fromCurrencyId, int toCurrencyId, DateTime? onDate = null);
}
