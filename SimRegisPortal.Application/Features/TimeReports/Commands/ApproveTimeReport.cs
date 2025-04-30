using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Factories;
using SimRegisPortal.Application.Features.TimeReports.Commands.Base;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Enums;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.TimeReports.Commands;

public sealed record ApproveTimeReportCommand(Guid Id)
    : ChangeTimeReportStatusCommand(Id);

internal sealed class ApproveTimeReportHandler(AppDbContext dbContext, ISalaryCalculatorFactory CalculatorFactory)
    : ChangeTimeReportStatusHandler<ApproveTimeReportCommand>(dbContext)
{
    protected override async Task UpdateEntity(TimeReport timeReport)
    {
        var calculator = CalculatorFactory.GetCalculator(timeReport.Employee.SalaryScheme);
        var paymentRequests = await calculator.CalculateAsync(timeReport);
        DbContext.AddRange(paymentRequests);

        timeReport.Status = TimeReportStatus.Approved;
        timeReport.UpdatedAt = DateTime.UtcNow;
    }

    protected override IQueryable<TimeReport> GetEntityQuery()
    {
        return DbContext.TimeReports
            .Include(r => r.Employee)
            .Include(r => r.Activities);
    }
}