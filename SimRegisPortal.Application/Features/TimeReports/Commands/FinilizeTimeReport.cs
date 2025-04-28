using SimRegisPortal.Application.Features.TimeReports.Commands.Base;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Enums;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.TimeReports.Commands;

public sealed record FinilizeTimeReportCommand(Guid Id)
    : ChangeTimeReportStatusCommand(Id);

internal sealed class FinilizeTimeReportHandler(AppDbContext dbContext)
    : ChangeTimeReportStatusHandler<FinilizeTimeReportCommand>(dbContext)
{
    protected override Task UpdateEntity(TimeReport timeReport)
    {
        timeReport.Status = TimeReportStatus.Finalized;
        timeReport.UpdatedAt = DateTime.UtcNow;

        return Task.CompletedTask;
    }
}