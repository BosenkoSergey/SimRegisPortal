using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.TimeReports.Commands.Base;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Enums;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.TimeReports.Commands;

public sealed record PayTimeReportCommand(Guid Id)
    : ChangeTimeReportStatusCommand(Id);

internal sealed class PayTimeReportHandler(AppDbContext dbContext)
    : ChangeTimeReportStatusHandler<PayTimeReportCommand>(dbContext)
{
    protected override Task UpdateEntity(TimeReport timeReport)
    {
        foreach (var paymentRequest in timeReport.PaymentRequests)
        {
            paymentRequest.IsPaid = true;
        }

        timeReport.Status = TimeReportStatus.Paid;
        timeReport.UpdatedAt = DateTime.UtcNow;

        return Task.CompletedTask;
    }

    protected override IQueryable<TimeReport> GetEntityQuery()
    {
        return DbContext.TimeReports
            .Include(r => r.PaymentRequests);
    }
}