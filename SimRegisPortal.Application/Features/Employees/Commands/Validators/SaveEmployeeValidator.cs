using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Extensions;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Employees.Commands.Validators;

public sealed class SaveEmployeeValidator
        : AbstractValidator<SaveEmployeeCommand>
{
    private readonly AppDbContext _dbContext;

    public SaveEmployeeValidator(AppDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Dto.FirstName)
            .NotEmpty().WithTemplate("Validation.Field.Required", "First Name");

        RuleFor(x => x.Dto.LastName)
            .NotEmpty().WithTemplate("Validation.Field.Required", "Last Name");

        RuleFor(x => x.Dto.HourlyRate)
            .GreaterThan(0).WithTemplate("Validation.Employee.HourlyRate");

        RuleFor(x => x.Dto)
            .Must(dto => dto.DismissalDate == null || dto.DismissalDate > dto.HireDate)
            .WithTemplate("Validation.Employee.DismissalDate");

        RuleFor(x => x)
            .CustomAsync(CustomCheckAsync);
    }

    private async Task CustomCheckAsync(
        SaveEmployeeCommand command,
        ValidationContext<SaveEmployeeCommand> context,
        CancellationToken cancellationToken)
    {
        var isUserLinked = await _dbContext.Employees
            .AnyAsync(r => r.Id != command.Dto.Id
                        && r.UserId == command.Dto.UserId,
                cancellationToken);
        if (isUserLinked)
        {
            throw new CommonException("Validation.Employee.User.AlreadyLinked");
        }
    }
}