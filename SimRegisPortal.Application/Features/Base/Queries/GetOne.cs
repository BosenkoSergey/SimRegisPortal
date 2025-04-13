using AutoMapper;
using MediatR;
using SimRegisPortal.Core.Entities.Base;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Base.Queries;

public record GetOneQuery<TResponse>
    : IRequest<TResponse>;

internal abstract class GetOneHandler<TQuery, TEntity, TResponse>(AppDbContext dbContext, IMapper mapper)
    : QueryHandler<TEntity>(dbContext, mapper), IRequestHandler<TQuery, TResponse>
    where TQuery : GetOneQuery<TResponse>
    where TEntity : BaseEntity
{
    public async Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken)
    {
        var entity = await GetEntity(query, cancellationToken);
        return CreateResponse(entity);
    }

    protected abstract Task<TEntity> GetEntity(TQuery query, CancellationToken cancellationToken);

    protected virtual TResponse CreateResponse(TEntity entity)
    {
        return Mapper.Map<TResponse>(entity);
    }
}