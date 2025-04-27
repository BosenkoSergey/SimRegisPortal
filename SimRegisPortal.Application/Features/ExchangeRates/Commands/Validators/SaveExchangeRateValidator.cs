using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.ExchangeRates.Commands.Validators;

public sealed class SaveExchangeRateValidator
        : AbstractValidator<SaveExchangeRateCommand>
{
    private readonly AppDbContext _dbContext;

    public SaveExchangeRateValidator(AppDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Dto.Rate)
            .GreaterThan(0).WithMessage("Validation.ExchangeRate.Negative");

        RuleFor(x => x)
            .CustomAsync(ValidateAsync);
    }

    private async Task ValidateAsync(
        SaveExchangeRateCommand command,
        ValidationContext<SaveExchangeRateCommand> context,
        CancellationToken cancellationToken)
    {
        var isExists = await _dbContext.ExchangeRates
            .AnyAsync(r => r.Id != command.Dto.Id
                        && r.FromCurrencyId == command.Dto.FromCurrencyId
                        && r.ToCurrencyId == command.Dto.ToCurrencyId
                        && r.Date == command.Dto.Date.Date,
                cancellationToken);
        if (isExists)
        {
            throw new CommonException("Validation.ExchangeRate.AlreadyExists");
        }
    }
}