﻿using MediatR;
using SimRegisPortal.Core.Entities.Base;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Base.Commands;

public abstract record DeleteCommand<TKey>(TKey Id)
    : IRequest;

internal abstract class BaseDeleteHandler<TCommand, TEntity, TKey>
    : CommandHandler<TEntity>, IRequestHandler<TCommand>
    where TCommand : DeleteCommand<TKey>
    where TEntity : BaseEntity<TKey>
{
    protected BaseDeleteHandler(AppDbContext dbContext)
        : base(dbContext) { }

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