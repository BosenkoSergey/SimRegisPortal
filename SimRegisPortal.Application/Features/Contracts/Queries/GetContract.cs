using AutoMapper;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Contracts.Queries;

public sealed record GetContractQuery(Guid Id)
    : GetByIdQuery<Guid, ContractDto>(Id);

internal sealed class GetContractHandler(AppDbContext dbContext, IMapper mapper)
    : GetByIdHandler<GetContractQuery, Contract, Guid, ContractDto>(dbContext, mapper);