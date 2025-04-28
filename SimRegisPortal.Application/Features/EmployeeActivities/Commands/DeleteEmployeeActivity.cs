using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.EmployeeActivities.Commands;

public sealed record DeleteEmployeeActivityCommand(Guid Id)
    : DeleteCommand<Guid>(Id);

internal sealed class DeleteEmployeeActivityHandler(AppDbContext dbContext)
    : DeleteHandler<DeleteEmployeeActivityCommand, EmployeeActivity, Guid>(dbContext);