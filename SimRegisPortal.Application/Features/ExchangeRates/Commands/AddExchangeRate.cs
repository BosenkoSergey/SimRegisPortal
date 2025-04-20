using AutoMapper;
using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Application.Models.ExchangeRate;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.ExchangeRates.Commands;

public sealed record AddExchangeRateCommand(ExchangeRateRequest Request)
    : AddCommand<ExchangeRateRequest, ExchangeRateResponse>(Request);

internal sealed class AddExchangeRateHandler(AppDbContext dbContext, IMapper mapper)
    : AddHandler<AddExchangeRateCommand, ExchangeRateRequest, ExchangeRate, ExchangeRateResponse>(dbContext, mapper);