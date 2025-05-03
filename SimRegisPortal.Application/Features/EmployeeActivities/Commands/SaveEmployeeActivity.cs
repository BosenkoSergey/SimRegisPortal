using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Enums;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.EmployeeActivities.Commands;

public sealed record SaveEmployeeActivityCommand(EmployeeActivityDto Dto)
    : SaveCommand<EmployeeActivityDto, Guid>(Dto);

internal sealed class SaveEmployeeActivityHandler(AppDbContext dbContext, IMapper mapper)
    : SaveHandler<SaveEmployeeActivityCommand, EmployeeActivityDto, EmployeeActivity, Guid>(dbContext, mapper)
{
    protected override async Task<EmployeeActivity> CreateEntity(SaveEmployeeActivityCommand command)
    {
        var entity = await base.CreateEntity(command);
        await UpdateTimeReport(entity);
        return entity;
    }

    protected override async Task UpdateEntity(EmployeeActivity entity, SaveEmployeeActivityCommand command)
    {
        await base.UpdateEntity(entity, command);
        await UpdateTimeReport(entity);
    }

    private async Task UpdateTimeReport(EmployeeActivity entity)
    {
        var reportYear = entity.Date.Year;
        var reportMonth = (Month)entity.Date.Month;

        var timeReport = await DbContext.TimeReports
            .AsNoTracking()
            .FirstOrDefaultAsync(r =>
                r.EmployeeId == entity.EmployeeId &&
                r.Year == reportYear &&
                r.Month == reportMonth);

        if (timeReport == null)
        {
            entity.TimeReport = new TimeReport()
            {
                EmployeeId = entity.EmployeeId,
                Year = reportYear,
                Month = reportMonth
            };
        }
        else if (timeReport.Status != TimeReportStatus.Draft)
        {
            throw new CommonException("Validation.TimeReport.Finilized");
        }
        else
        {
            entity.TimeReportId = timeReport.Id;
        }
    }
}