using AutoMapper;
using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Application.Models.ExchangeRate;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.ExchangeRates.Commands;

public sealed record EditExchangeRateCommand(int Id, ExchangeRateRequest Request)
    : EditCommand<int, ExchangeRateRequest, ExchangeRateResponse>(Id, Request);

internal sealed class EditExchangeRateHandler(AppDbContext dbContext, IMapper mapper)
    : EditHandler<EditExchangeRateCommand, ExchangeRateRequest, ExchangeRate, int, ExchangeRateResponse>(dbContext, mapper);