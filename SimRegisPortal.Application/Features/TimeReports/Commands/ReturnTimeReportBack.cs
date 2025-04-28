using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.TimeReports.Commands.Base;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Enums;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.TimeReports.Commands;

public sealed record ReturnTimeReportBackCommand(Guid Id)
    : ChangeTimeReportStatusCommand(Id);

internal sealed class ReturnTimeReportBackHandler(AppDbContext dbContext)
    : ChangeTimeReportStatusHandler<ReturnTimeReportBackCommand>(dbContext)
{
    protected override Task UpdateEntity(TimeReport timeReport)
    {
        timeReport.PaymentRequests.Clear();
        timeReport.Status = TimeReportStatus.Draft;
        timeReport.UpdatedAt = DateTime.UtcNow;

        return Task.CompletedTask;
    }

    protected override IQueryable<TimeReport> GetEntityQuery()
    {
        return DbContext.TimeReports
            .Include(r => r.PaymentRequests);
    }
}