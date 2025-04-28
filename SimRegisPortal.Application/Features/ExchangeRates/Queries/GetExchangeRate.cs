using AutoMapper;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.ExchangeRate;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.ExchangeRates.Queries;

public sealed record GetExchangeRateQuery(int Id)
    : GetByIdQuery<int, ExchangeRateDto>(Id);

internal sealed class GetExchangeRateHandler(AppDbContext dbContext, IMapper mapper)
    : GetByIdHandler<GetExchangeRateQuery, ExchangeRate, int, ExchangeRateDto>(dbContext, mapper);