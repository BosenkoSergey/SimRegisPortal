using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Contracts.Commands;

public sealed record DeleteContractCommand(Guid Id)
    : DeleteCommand<Guid>(Id);

internal sealed class DeleteContractHandler(AppDbContext dbContext)
    : DeleteHandler<DeleteContractCommand, Contract, Guid>(dbContext);