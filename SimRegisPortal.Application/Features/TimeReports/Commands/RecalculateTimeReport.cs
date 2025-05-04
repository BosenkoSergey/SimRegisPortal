using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Factories;
using SimRegisPortal.Application.Features.TimeReports.Commands.Base;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.TimeReports.Commands;

public sealed record RecalculateTimeReportCommand(Guid Id)
    : EditTimeReportCommand(Id);

internal sealed class RecalculateTimeReportHandler(
        AppDbContext dbContext,
        ISalaryCalculatorFactory CalculatorFactory)
    : EditTimeReportHandler<RecalculateTimeReportCommand>(dbContext)
{
    protected override async Task UpdateEntity(TimeReport timeReport, RecalculateTimeReportCommand command)
    {
        timeReport.PaymentRequests.Clear();
        var calculator = CalculatorFactory.GetCalculator(timeReport.Employee.SalaryScheme);
        var paymentRequests = await calculator.CalculateAsync(timeReport);
        DbContext.AddRange(paymentRequests);
    }

    protected override IQueryable<TimeReport> GetEntityQuery()
    {
        return DbContext.TimeReports
            .AsSplitQuery()
            .Include(r => r.Employee)
            .Include(r => r.Activities)
            .Include(r => r.PaymentRequests);
    }
}