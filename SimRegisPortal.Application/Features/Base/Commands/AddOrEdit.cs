using AutoMapper;
using MediatR;
using SimRegisPortal.Core.Entities.Base;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Base.Commands;

public abstract record AddOrEditCommand<TRequest, TResponse>(TRequest Request)
    : IRequest<TResponse>;

internal abstract class AddOrEditHandler<TCommand, TRequest, TEntity, TResponse>(AppDbContext dbContext, IMapper mapper)
    : CommandHandler<TEntity>(dbContext), IRequestHandler<TCommand, TResponse>
    where TCommand : AddOrEditCommand<TRequest, TResponse>
    where TEntity : BaseEntity
{
    protected readonly IMapper Mapper = mapper;

    public async Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var entity = await GetEntity(command, cancellationToken);
        await AddOrEditEntity(entity, command, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
        return CreateResponse(entity);
    }

    protected abstract Task<TEntity> GetEntity(TCommand command, CancellationToken cancellationToken);
    protected abstract Task AddOrEditEntity(TEntity entity, TCommand command, CancellationToken cancellationToken);

    protected virtual TResponse CreateResponse(TEntity entity)
    {
        return Mapper.Map<TResponse>(entity);
    }
}