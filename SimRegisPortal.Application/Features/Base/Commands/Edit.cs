using AutoMapper;
using SimRegisPortal.Core.Entities.Base;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Base.Commands;

public abstract record EditCommand<TKey, TRequest, TResponse>(TKey Id, TRequest Request)
    : AddOrEditCommand<TRequest, TResponse>(Request);

internal abstract class EditHandler<TCommand, TRequest, TEntity, TKey, TResponse>
    : AddOrEditHandler<TCommand, TRequest, TEntity, TResponse>
    where TCommand : EditCommand<TKey, TRequest, TResponse>
    where TEntity : BaseEntity<TKey>
{
    protected EditHandler(AppDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper) { }

    protected override async Task<TEntity> GetEntity(TCommand command, CancellationToken cancellationToken)
    {
        return await DbSet.FindAsync(command.Id, cancellationToken)
            ?? throw new ResourceNotFoundException(typeof(TEntity).Name);
    }

    protected override Task AddOrEditEntity(TEntity entity, TCommand command, CancellationToken cancellationToken)
    {
        Mapper.Map(command.Request, entity);
        return Task.CompletedTask;
    }
}