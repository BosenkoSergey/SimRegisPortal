using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Projects.Queries;

public sealed record GetProjectsLookupQuery
    : GetLookupQuery<Guid>;

internal sealed class GetProjectsLookupHandler(AppDbContext dbContext)
    : GetLookupHandler<GetProjectsLookupQuery, Project, Guid>(dbContext)
{
    protected override async Task<Dictionary<Guid, string>> GetLookupEntities()
    {
        return await Repository.ToDictionaryAsync(e => e.Id, e => e.Name);
    }
}
