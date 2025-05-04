using MediatR;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.TimeReports.Commands.Base;

public abstract record EditTimeReportCommand(Guid Id)
    : IRequest;

internal abstract class EditTimeReportHandler<TCommand>(AppDbContext dbContext)
    : IRequestHandler<TCommand>
    where TCommand : EditTimeReportCommand
{
    protected readonly AppDbContext DbContext = dbContext;

    public async Task Handle(TCommand command, CancellationToken cancellationToken)
    {
        var timeReport = await GetEntity(command.Id, cancellationToken);
        await UpdateEntity(timeReport, command);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    protected abstract Task UpdateEntity(TimeReport timeReport, TCommand command);

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