using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Currencies.Queries;

public sealed record GetCurrenciesLookupQuery
    : GetLookupQuery<int>;

internal sealed class GetCurrenciesLookupHandler(AppDbContext dbContext)
    : GetLookupHandler<GetCurrenciesLookupQuery, Currency, int>(dbContext)
{
    protected override async Task<Dictionary<int, string>> GetLookupEntities()
    {
        return await Repository.ToDictionaryAsync(e => e.Id, e => $"{e.Code} ({e.Name})");
    }
}
