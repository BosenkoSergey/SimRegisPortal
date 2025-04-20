using MediatR;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Models.ExchangeRate;
using SimRegisPortal.Application.Services.Interfaces;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.ExchangeRates.Commands;

public sealed record ImportExchangeRatesCommand(ImportExchangeRatesRequest Request)
    : IRequest;

internal sealed class ImportExchangeRatesHandler(
        AppDbContext DbContext,
        IPrivatBankService BankService)
    : IRequestHandler<ImportExchangeRatesCommand>
{
    public async Task Handle(ImportExchangeRatesCommand command, CancellationToken cancellationToken)
    {
        var date = command.Request.ImportDate.Date;

        var bankRates = await BankService.GetRatesAsync(date, cancellationToken);

        var currencies = await DbContext.Currencies.ToListAsync(cancellationToken);
        var currencyMap = currencies.ToDictionary(c => c.Code, c => c.Id);

        var existingRates = await DbContext.ExchangeRates
            .Where(r => r.Date == date)
            .ToListAsync(cancellationToken);

        foreach (var rate in bankRates)
        {
            if (!currencyMap.TryGetValue(rate.BaseCurrency, out var fromCurrencyId) ||
                !currencyMap.TryGetValue(rate.Currency, out var toCurrencyId) ||
                fromCurrencyId == toCurrencyId)
            {
                continue;
            }

            Upsert(existingRates, fromCurrencyId, toCurrencyId, date, rate.SaleRateNB);
            Upsert(existingRates, toCurrencyId, fromCurrencyId, date, 1 / rate.PurchaseRateNB);
        }

        await DbContext.SaveChangesAsync(cancellationToken);
    }

    private void Upsert(
        List<ExchangeRate> existing,
        int fromCurrencyId,
        int toCurrencyId,
        DateTime date,
        decimal rate)
    {
        var existingRate = existing.FirstOrDefault(x =>
            x.FromCurrencyId == fromCurrencyId &&
            x.ToCurrencyId == toCurrencyId);

        if (existingRate != null)
        {
            existingRate.Rate = rate;
        }
        else
        {
            DbContext.ExchangeRates.Add(new ExchangeRate
            {
                FromCurrencyId = fromCurrencyId,
                ToCurrencyId = toCurrencyId,
                Date = date,
                Rate = rate
            });
        }
    }
}
