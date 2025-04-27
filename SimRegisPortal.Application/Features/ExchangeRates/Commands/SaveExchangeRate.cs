using AutoMapper;
using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Application.Models.ExchangeRate;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.ExchangeRates.Commands;

public sealed record SaveExchangeRateCommand(ExchangeRateDto Dto)
    : SaveCommand<ExchangeRateDto, int>(Dto);

internal sealed class SaveExchangeRateHandler(AppDbContext dbContext, IMapper mapper)
    : SaveHandler<SaveExchangeRateCommand, ExchangeRateDto, ExchangeRate, int>(dbContext, mapper);