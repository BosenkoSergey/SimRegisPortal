using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Application.Models.Entities.Related;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.TimeReports.Queries;

public sealed record GetTimeReportsQuery(TimeReportQueryParams QueryParams)
    : GetManyQuery<TimeReportDto>;

internal sealed class GetTimeReportsHandler(AppDbContext dbContext, IMapper mapper)
    : GetManyHandler<GetTimeReportsQuery, TimeReport, TimeReportDto>(dbContext, mapper)
{
    protected override async Task<IEnumerable<TimeReport>> GetEntities(GetTimeReportsQuery query, CancellationToken cancellationToken)
    {
        var entitiesQuery = GetEntitiesQuery();

        if (query.QueryParams.Year.HasValue)
        {
            entitiesQuery = entitiesQuery.Where(r =>
                r.Year == query.QueryParams.Year.Value);
        }
        if (query.QueryParams.Month.HasValue)
        {
            entitiesQuery = entitiesQuery.Where(r =>
                r.Month == query.QueryParams.Month.Value);
        }
        if (query.QueryParams.EmployeeId.HasValue)
        {
            entitiesQuery = entitiesQuery.Where(r =>
                r.EmployeeId == query.QueryParams.EmployeeId.Value);
        }
        if (query.QueryParams.Status.HasValue)
        {
            entitiesQuery = entitiesQuery.Where(r =>
                r.Status == query.QueryParams.Status.Value);
        }

        return await entitiesQuery
            .OrderByDescending(r => r.Year)
                .ThenByDescending(r => r.Month)
                .ThenByDescending(r => r.EmployeeId)
            .ToListAsync(cancellationToken);
    }

    protected override IEnumerable<TimeReportDto> CreateResponse(IEnumerable<TimeReport> entities)
    {
        var response = base.CreateResponse(entities);
        var timeReportIds = response.Select(r => r.Id).ToList();

        var hoursByTimeReportId = Repository
            .SelectMany(r => r.Activities)
            .Where(a => timeReportIds.Contains(a.TimeReportId))
            .GroupBy(a => a.TimeReportId)
            .Select(g => new
            {
                TimeReportId = g.Key,
                TotalHours = g.Sum(a => a.Hours)
            })
            .ToDictionary(x => x.TimeReportId, x => x.TotalHours);

        foreach (var report in response)
        {
            report.Hours = hoursByTimeReportId.GetValueOrDefault(report.Id, 0);
        }

        return response;
    }
}
