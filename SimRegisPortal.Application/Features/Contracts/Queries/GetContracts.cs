using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Application.Models.Entities.Related;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Contracts.Queries;

public sealed record GetContractsQuery(ContractQueryParams QueryParams)
    : GetManyQuery<ContractDto>;

internal sealed class GetContractsHandler(AppDbContext dbContext, IMapper mapper)
    : GetManyHandler<GetContractsQuery, Contract, ContractDto>(dbContext, mapper)
{
    protected override async Task<IEnumerable<Contract>> GetEntities(GetContractsQuery query, CancellationToken cancellationToken)
    {
        var entitiesQuery = GetEntitiesQuery();

        if (query.QueryParams.ProjectId.HasValue)
        {
            entitiesQuery = entitiesQuery.Where(r =>
                r.ProjectId == query.QueryParams.ProjectId.Value);
        }
        if (query.QueryParams.EmployeeId.HasValue)
        {
            entitiesQuery = entitiesQuery.Where(r =>
                r.EmployeeId == query.QueryParams.EmployeeId.Value);
        }

        return await entitiesQuery
            .OrderBy(r => r.ProjectId).ThenBy(r => r.EmployeeId).ThenBy(r => r.Id)
            .ToListAsync(cancellationToken);
    }
}
