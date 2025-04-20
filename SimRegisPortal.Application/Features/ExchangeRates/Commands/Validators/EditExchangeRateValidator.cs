using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.ExchangeRates.Commands.Validators;

public sealed class EditExchangeRateValidator
        : AbstractValidator<EditExchangeRateCommand>
{
    private readonly AppDbContext _dbContext;

    public EditExchangeRateValidator(AppDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Request.Rate)
            .GreaterThan(0).WithMessage("Validation.ExchangeRate.Negative");

        RuleFor(x => x)
            .CustomAsync(ValidateAsync);
    }

    private async Task ValidateAsync(
        EditExchangeRateCommand command,
        ValidationContext<EditExchangeRateCommand> context,
        CancellationToken cancellationToken)
    {
        var isExists = await _dbContext.ExchangeRates
            .AnyAsync(r => r.Id != command.Id
                        && r.FromCurrencyId == command.Request.FromCurrencyId
                        && r.ToCurrencyId == command.Request.ToCurrencyId
                        && r.Date == command.Request.Date.Date,
                cancellationToken);
        if (isExists)
        {
            throw new CommonException("Validation.ExchangeRate.AlreadyExists");
        }
    }
}