using AutoMapper;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.TimeReports.Queries;

public sealed record GetTimeReportsQuery
    : GetManyQuery<TimeReportDto>;

internal sealed class GetTimeReportsHandler(AppDbContext dbContext, IMapper mapper)
    : GetManyHandler<GetTimeReportsQuery, TimeReport, TimeReportDto>(dbContext, mapper)
{
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
