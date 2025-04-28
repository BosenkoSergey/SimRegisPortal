using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Contracts.Commands.Validators;

public sealed class SaveContractValidator
        : AbstractValidator<SaveContractCommand>
{
    private readonly AppDbContext _dbContext;

    public SaveContractValidator(AppDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x)
            .CustomAsync(CustomCheckAsync);
    }

    private async Task CustomCheckAsync(
        SaveContractCommand command,
        ValidationContext<SaveContractCommand> context,
        CancellationToken cancellationToken)
    {
        var isNumberExists = await _dbContext.Contracts
            .AnyAsync(c => c.Number == command.Dto.Number
                        && c.Id != command.Dto.Id, cancellationToken);
        if (isNumberExists)
        {
            throw new CommonException("Validation.Contract.Number.AlreadyExists");
        }

        var isRangeExists = await _dbContext.Contracts
            .AnyAsync(c => c.ProjectId == command.Dto.ProjectId
                        && c.EmployeeId == command.Dto.EmployeeId
                        && c.DateFrom <= command.Dto.DateTo
                        && c.DateTo >= command.Dto.DateFrom
                        && c.Id != command.Dto.Id, cancellationToken);
        if (isRangeExists)
        {
            throw new CommonException("Validation.Contract.Range.AlreadyExists");
        }
    }
}