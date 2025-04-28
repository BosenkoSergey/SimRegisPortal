using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Projects.Commands;

public sealed record DeleteProjectCommand(Guid Id)
    : DeleteCommand<Guid>(Id);

internal sealed class DeleteProjectHandler(AppDbContext dbContext)
    : DeleteHandler<DeleteProjectCommand, Project, Guid>(dbContext);