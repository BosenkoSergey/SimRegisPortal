using AutoMapper;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Currencies.Queries;

public sealed record GetCurrenciesQuery
    : GetManyQuery<CurrencyDto>;

internal sealed class GetCurrenciesHandler(AppDbContext dbContext, IMapper mapper)
    : GetManyHandler<GetCurrenciesQuery, Currency, CurrencyDto>(dbContext, mapper);
