using SimRegisPortal.Application.Features.TimeReports.Commands.Base;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Enums;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.TimeReports.Commands;

public sealed record ChangeTimeReportStatusCommand(Guid Id, TimeReportStatus Status)
    : EditTimeReportCommand(Id);

internal sealed class ChangeTimeReportStatusHandler(AppDbContext dbContext)
    : EditTimeReportHandler<ChangeTimeReportStatusCommand>(dbContext)
{
    protected override Task UpdateEntity(TimeReport timeReport, ChangeTimeReportStatusCommand command)
    {
        timeReport.Status = command.Status;
        timeReport.UpdatedAt = DateTime.UtcNow;
        return Task.CompletedTask;
    }
}