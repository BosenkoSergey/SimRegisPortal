using AutoMapper;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Currency;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Currencies.Queries;

public sealed record GetCurrenciesQuery
    : GetManyQuery<CurrencyResponse>;

internal sealed class GetCurrenciesHandler(AppDbContext dbContext, IMapper mapper)
    : GetManyHandler<GetCurrenciesQuery, Currency, CurrencyResponse>(dbContext, mapper);
