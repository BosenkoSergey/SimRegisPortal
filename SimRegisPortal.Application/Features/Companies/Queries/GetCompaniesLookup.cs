using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Companies.Queries;

public sealed record GetCompaniesLookupQuery
    : GetLookupQuery<Guid>;

internal sealed class GetCompaniesLookupHandler(AppDbContext dbContext)
    : GetLookupHandler<GetCompaniesLookupQuery, Company, Guid>(dbContext)
{
    protected override async Task<Dictionary<Guid, string>> GetLookupEntities()
    {
        return await Repository.ToDictionaryAsync(e => e.Id, e => e.Name);
    }
}
