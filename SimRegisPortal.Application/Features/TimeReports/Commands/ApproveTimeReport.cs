using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.TimeReports.Commands.Base;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Enums;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.TimeReports.Commands;

public sealed record ApproveTimeReportCommand(Guid Id)
    : ChangeTimeReportStatusCommand(Id);

internal sealed class ApproveTimeReportHandler(AppDbContext dbContext)
    : ChangeTimeReportStatusHandler<ApproveTimeReportCommand>(dbContext)
{
    protected override Task UpdateEntity(TimeReport timeReport)
    {
        // TODO: Generate PRs

        timeReport.Status = TimeReportStatus.Approved;
        timeReport.UpdatedAt = DateTime.UtcNow;

        return Task.CompletedTask;
    }

    protected override IQueryable<TimeReport> GetEntityQuery()
    {
        return DbContext.TimeReports
            .Include(r => r.Activities);
    }
}