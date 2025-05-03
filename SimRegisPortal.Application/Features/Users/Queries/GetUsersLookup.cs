using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Users.Queries;

public sealed record GetUsersLookupQuery
    : GetLookupQuery<Guid>;

internal sealed class GetUsersLookupHandler(AppDbContext dbContext)
    : GetLookupHandler<GetUsersLookupQuery, User, Guid>(dbContext)
{
    protected override async Task<Dictionary<Guid, string>> GetLookupEntities()
    {
        return await Repository.ToDictionaryAsync(e => e.Id, e => e.FullName);
    }
}
