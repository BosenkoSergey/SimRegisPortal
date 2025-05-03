using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Employees.Commands;

public sealed record DeleteEmployeeCommand(Guid Id)
    : DeleteCommand<Guid>(Id);

internal sealed class DeleteEmployeeHandler(AppDbContext dbContext)
    : DeleteHandler<DeleteEmployeeCommand, Employee, Guid>(dbContext);