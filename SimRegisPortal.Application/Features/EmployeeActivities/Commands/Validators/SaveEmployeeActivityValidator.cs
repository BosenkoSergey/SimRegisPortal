using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Extensions;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.EmployeeActivities.Commands.Validators;

public sealed class SaveEmployeeActivityValidator
        : AbstractValidator<SaveEmployeeActivityCommand>
{
    private readonly AppDbContext _dbContext;

    public SaveEmployeeActivityValidator(AppDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Dto.Hours)
            .GreaterThan(0).WithTemplate("Validation.EmployeeActivity.NegativeDuration");

        RuleFor(x => x)
            .CustomAsync(CustomCheckAsync);
    }

    private async Task CustomCheckAsync(
        SaveEmployeeActivityCommand command,
        ValidationContext<SaveEmployeeActivityCommand> context,
        CancellationToken cancellationToken)
    {
        var isInternalProject = await _dbContext.Projects
            .AnyAsync(p => p.Id == command.Dto.ProjectId
                        && p.IsInternal,
                cancellationToken);
        if (!isInternalProject)
        {
            var isContracted = await _dbContext.Contracts
            .AnyAsync(r => r.ProjectId == command.Dto.ProjectId
                        && r.EmployeeId == command.Dto.EmployeeId
                        && r.DateFrom <= command.Dto.Date!.Value.Date
                        && r.DateTo >= command.Dto.Date!.Value.Date,
                cancellationToken);
            if (!isContracted)
            {
                throw new CommonException("Validation.EmployeeActivity.NotContracted");
            }
        }
    }
}