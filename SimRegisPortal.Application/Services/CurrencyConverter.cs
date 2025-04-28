using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Services.Interfaces;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Services;

public sealed class CurrencyConverter(AppDbContext DbContext)
    : ICurrencyConverter
{
    public async Task<decimal> ConvertAsync(decimal amount, int fromCurrencyId, int toCurrencyId, DateTime? onDate = null)
    {
        if (fromCurrencyId == toCurrencyId)
        {
            return amount;
        }

        var date = (onDate ?? DateTime.UtcNow).Date;

        var rate = await DbContext.ExchangeRates
            .Where(r => r.FromCurrencyId == fromCurrencyId
                        && r.ToCurrencyId == toCurrencyId
                        && r.Date <= date)
            .OrderByDescending(r => r.Date)
            .FirstOrDefaultAsync();

        if (rate != null)
        {
            return amount * rate.Rate;
        }

        var inverseRate = await DbContext.ExchangeRates
            .Where(r => r.FromCurrencyId == toCurrencyId
                        && r.ToCurrencyId == fromCurrencyId
                        && r.Date <= date)
            .OrderByDescending(r => r.Date)
            .FirstOrDefaultAsync();

        if (inverseRate != null)
        {
            return amount / inverseRate.Rate;
        }

        throw new CommonException("Validation.ExchangeRate.NotFound");
    }
}
