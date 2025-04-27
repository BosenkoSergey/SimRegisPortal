using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.ExchangeRates.Commands;

public sealed record DeleteExchangeRateCommand(int Id)
    : DeleteCommand<int>(Id);

internal sealed class DeleteExchangeRateHandler(AppDbContext dbContext)
    : DeleteHandler<DeleteExchangeRateCommand, ExchangeRate, int>(dbContext);