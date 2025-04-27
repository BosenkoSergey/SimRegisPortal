using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Core.Entities.Base;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Base.Queries;

public record GetManyQuery<TDto>
    : IRequest<IEnumerable<TDto>>;

internal abstract class GetManyHandler<TQuery, TEntity, TDto>(AppDbContext dbContext, IMapper mapper)
    : QueryHandler<TEntity>(dbContext, mapper), IRequestHandler<TQuery, IEnumerable<TDto>>
    where TQuery : GetManyQuery<TDto>
    where TEntity : BaseEntity
{
    public async Task<IEnumerable<TDto>> Handle(TQuery query, CancellationToken cancellationToken)
    {
        var entities = await GetEntities(query, cancellationToken);
        return CreateResponse(entities);
    }

    protected virtual async Task<IEnumerable<TEntity>> GetEntities(TQuery query, CancellationToken cancellationToken)
    {
        return await GetEntitiesQuery()
            .ToListAsync(cancellationToken);
    }

    protected virtual IEnumerable<TDto> CreateResponse(IEnumerable<TEntity> entities)
    {
        return Mapper.Map<IEnumerable<TDto>>(entities);
    }
}