using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.ExchangeRate;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.ExchangeRates.Queries;

public sealed record GetExchangeRatesQuery(ExchangeRateQueryParams QueryParams)
    : GetManyQuery<ExchangeRateDto>;

internal sealed class GetExchangeRatesHandler(AppDbContext dbContext, IMapper mapper)
    : GetManyHandler<GetExchangeRatesQuery, ExchangeRate, ExchangeRateDto>(dbContext, mapper)
{
    protected override async Task<IEnumerable<ExchangeRate>> GetEntities(GetExchangeRatesQuery query, CancellationToken cancellationToken)
    {
        var entitiesQuery = GetEntitiesQuery();

        if (query.QueryParams.FromCurrencyId.HasValue)
        {
            entitiesQuery = entitiesQuery.Where(r =>
                r.FromCurrencyId == query.QueryParams.FromCurrencyId.Value);
        }
        if (query.QueryParams.ToCurrencyId.HasValue)
        {
            entitiesQuery = entitiesQuery.Where(r =>
                r.ToCurrencyId == query.QueryParams.ToCurrencyId.Value);
        }
        if (query.QueryParams.DateFrom.HasValue)
        {
            entitiesQuery = entitiesQuery.Where(r =>
                r.Date >= query.QueryParams.DateFrom.Value.Date);
        }
        if (query.QueryParams.DateTo.HasValue)
        {
            entitiesQuery = entitiesQuery.Where(r =>
                r.Date <= query.QueryParams.DateTo.Value.Date);
        }

        return await entitiesQuery.ToListAsync(cancellationToken);
    }
}
