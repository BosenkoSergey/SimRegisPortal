using AutoMapper;
using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Application.Models.ExchangeRate;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.ExchangeRates.Commands;

public sealed record AddExchangeRateCommand(ExchangeRateDto Request)
    : AddCommand<ExchangeRateDto, ExchangeRateDto>(Request);

internal sealed class AddExchangeRateHandler(AppDbContext dbContext, IMapper mapper)
    : AddHandler<AddExchangeRateCommand, ExchangeRateDto, ExchangeRate, ExchangeRateDto>(dbContext, mapper);