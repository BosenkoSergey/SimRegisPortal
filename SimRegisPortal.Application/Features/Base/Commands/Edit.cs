using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Core.Entities.Base;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Base.Commands;

public abstract record EditCommand<TKey, TRequest, TResponse>(TKey Id, TRequest Request)
    : AddOrEditCommand<TRequest, TResponse>(Request);

internal abstract class EditHandler<TCommand, TRequest, TEntity, TKey, TResponse>(AppDbContext dbContext, IMapper mapper)
    : AddOrEditHandler<TCommand, TRequest, TEntity, TResponse>(dbContext, mapper)
    where TCommand : EditCommand<TKey, TRequest, TResponse>
    where TEntity : BaseEntity<TKey>
{
    protected override async Task<TEntity> GetEntity(TCommand command, CancellationToken cancellationToken)
    {
        return await GetEntityQuery()
                .Where(e => e.Id!.Equals(e.Id))
                .FirstOrDefaultAsync(cancellationToken)
            ?? throw new ResourceNotFoundException(typeof(TEntity).Name);
    }

    protected virtual IQueryable<TEntity> GetEntityQuery()
    {
        return DbSet;
    }

    protected override Task AddOrEditEntity(TEntity entity, TCommand command, CancellationToken cancellationToken)
    {
        Mapper.Map(command.Request, entity);
        return Task.CompletedTask;
    }
}