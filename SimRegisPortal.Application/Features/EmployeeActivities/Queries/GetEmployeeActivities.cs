using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Application.Models.Entities.Related;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.EmployeeActivities.Queries;

public sealed record GetEmployeeActivitiesQuery(EmployeeActivityQueryParams QueryParams)
    : GetManyQuery<EmployeeActivityDto>;

internal sealed class GetEmployeeActivitiesHandler(AppDbContext dbContext, IMapper mapper)
    : GetManyHandler<GetEmployeeActivitiesQuery, EmployeeActivity, EmployeeActivityDto>(dbContext, mapper)
{
    protected override async Task<IEnumerable<EmployeeActivity>> GetEntities(GetEmployeeActivitiesQuery query, CancellationToken cancellationToken)
    {
        var entitiesQuery = GetEntitiesQuery();

        if (query.QueryParams.TimeReportId.HasValue)
        {
            entitiesQuery = entitiesQuery.Where(r =>
                r.TimeReportId == query.QueryParams.TimeReportId.Value);
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
            .OrderByDescending(r => r.Date)
            .ToListAsync(cancellationToken);
    }
}