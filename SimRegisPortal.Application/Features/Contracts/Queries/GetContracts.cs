using AutoMapper;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Contracts.Queries;

public sealed record GetContractsQuery
    : GetManyQuery<ContractDto>;

internal sealed class GetContractsHandler(AppDbContext dbContext, IMapper mapper)
    : GetManyHandler<GetContractsQuery, Contract, ContractDto>(dbContext, mapper);
