using AutoMapper;
using SimRegisPortal.Core.Entities.Base;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Base.Commands;

public abstract record AddCommand<TRequest, TResponse>(TRequest Request)
    : AddOrEditCommand<TRequest, TResponse>(Request);

internal abstract class AddHandler<TCommand, TRequest, TEntity, TResponse>
    : AddOrEditHandler<TCommand, TRequest, TEntity, TResponse>
    where TCommand : AddCommand<TRequest, TResponse>
    where TEntity : BaseEntity
{
    protected AddHandler(AppDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper) { }

    protected override Task<TEntity> GetEntity(TCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<TEntity>(command.Request);
        return Task.FromResult(entity);
    }

    protected override async Task AddOrEditEntity(TEntity entity, TCommand command, CancellationToken cancellationToken)
    {
        await DbSet.AddAsync(entity, cancellationToken);
    }
}