using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Application.Models.Entities.Related;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.SystemLogs.Queries;

public sealed record GetSystemLogsQuery(SystemLogQueryParams QueryParams)
    : GetManyQuery<SystemLogDto>;

internal sealed class GetSystemLogsHandler(AppDbContext dbContext, IMapper mapper)
    : GetManyHandler<GetSystemLogsQuery, SystemLog, SystemLogDto>(dbContext, mapper)
{
    protected override async Task<IEnumerable<SystemLog>> GetEntities(GetSystemLogsQuery query, CancellationToken cancellationToken)
    {
        var entitiesQuery = Repository;

        if (query.QueryParams.DateFrom.HasValue)
        {
            entitiesQuery = entitiesQuery.Where(sl =>
                sl.TimeStamp >= query.QueryParams.DateFrom.Value.Date);
        }
        if (query.QueryParams.DateTo.HasValue)
        {
            entitiesQuery = entitiesQuery.Where(sl =>
                sl.TimeStamp <= query.QueryParams.DateTo.Value.Date);
        }

        return await entitiesQuery
            .OrderByDescending(sl => sl.TimeStamp)
            .ToListAsync(cancellationToken);
    }
}
