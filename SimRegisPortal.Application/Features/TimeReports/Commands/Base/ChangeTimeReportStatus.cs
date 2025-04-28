using MediatR;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.TimeReports.Commands.Base;

public abstract record ChangeTimeReportStatusCommand(Guid Id)
    : IRequest;

internal abstract class ChangeTimeReportStatusHandler<TCommand>(AppDbContext dbContext)
    : IRequestHandler<TCommand>
    where TCommand : ChangeTimeReportStatusCommand
{
    protected readonly AppDbContext DbContext = dbContext;

    public async Task Handle(TCommand command, CancellationToken cancellationToken)
    {
        var timeReport = await GetEntity(command.Id, cancellationToken);
        await UpdateEntity(timeReport);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    protected abstract Task UpdateEntity(TimeReport timeReport);

    protected virtual async Task<TimeReport> GetEntity(Guid id, CancellationToken cancellationToken)
    {
        return await GetEntityQuery()
                .Where(e => e.Id!.Equals(id))
                .FirstOrDefaultAsync(cancellationToken)
            ?? throw new ResourceNotFoundException(nameof(TimeReport));
    }

    protected virtual IQueryable<TimeReport> GetEntityQuery()
    {
        return DbContext.TimeReports;
    }
}