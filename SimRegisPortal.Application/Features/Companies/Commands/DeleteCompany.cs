using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Companies.Commands;

public sealed record DeleteCompanyCommand(Guid Id)
    : DeleteCommand<Guid>(Id);

internal sealed class DeleteCompanyHandler(AppDbContext dbContext)
    : DeleteHandler<DeleteCompanyCommand, Company, Guid>(dbContext);