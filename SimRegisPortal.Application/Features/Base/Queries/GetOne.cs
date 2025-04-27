using AutoMapper;
using MediatR;
using SimRegisPortal.Core.Entities.Base;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Base.Queries;

public record GetOneQuery<TDto>
    : IRequest<TDto>;

internal abstract class GetOneHandler<TQuery, TEntity, TDto>(AppDbContext dbContext, IMapper mapper)
    : QueryHandler<TEntity>(dbContext, mapper), IRequestHandler<TQuery, TDto>
    where TQuery : GetOneQuery<TDto>
    where TEntity : BaseEntity
{
    public async Task<TDto> Handle(TQuery query, CancellationToken cancellationToken)
    {
        var entity = await GetEntity(query, cancellationToken);
        return CreateResponse(entity);
    }

    protected abstract Task<TEntity> GetEntity(TQuery query, CancellationToken cancellationToken);

    protected virtual TDto CreateResponse(TEntity entity)
    {
        return Mapper.Map<TDto>(entity);
    }
}