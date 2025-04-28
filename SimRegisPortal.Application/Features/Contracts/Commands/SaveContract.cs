using AutoMapper;
using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Contracts.Commands;

public sealed record SaveContractCommand(ContractDto Dto)
    : SaveCommand<ContractDto, Guid>(Dto);

internal sealed class SaveContractHandler(AppDbContext dbContext, IMapper mapper)
    : SaveHandler<SaveContractCommand, ContractDto, Contract, Guid>(dbContext, mapper);