using MediatR;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Base.Commands;

public abstract record DeleteCommand<TKey>(TKey Id)
    : IRequest;

internal abstract class DeleteHandler<TCommand, TEntity, TKey>(AppDbContext dbContext)
    : CommandHandler<TEntity>(dbContext), IRequestHandler<TCommand>
    where TCommand : DeleteCommand<TKey>
    where TEntity : BaseEntity<TKey>
{
    public async Task Handle(TCommand command, CancellationToken cancellationToken)
    {
        var entity = await GetEntity(command, cancellationToken);
        await RemoveEntity(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    protected async Task<TEntity> GetEntity(TCommand command, CancellationToken cancellationToken)
    {
        return await DbSet.FindAsync(command.Id, cancellationToken)
            ?? throw new ResourceNotFoundException(typeof(TEntity).Name);
    }

    protected virtual Task RemoveEntity(TEntity entity)
    {
        DbSet.Remove(entity);
        return Task.CompletedTask;
    }
}