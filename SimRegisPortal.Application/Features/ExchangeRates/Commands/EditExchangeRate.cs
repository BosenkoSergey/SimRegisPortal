using AutoMapper;
using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Application.Models.ExchangeRate;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.ExchangeRates.Commands;

public sealed record EditExchangeRateCommand(int Id, ExchangeRateDto Request)
    : EditCommand<int, ExchangeRateDto, ExchangeRateDto>(Id, Request);

internal sealed class EditExchangeRateHandler(AppDbContext dbContext, IMapper mapper)
    : EditHandler<EditExchangeRateCommand, ExchangeRateDto, ExchangeRate, int, ExchangeRateDto>(dbContext, mapper);