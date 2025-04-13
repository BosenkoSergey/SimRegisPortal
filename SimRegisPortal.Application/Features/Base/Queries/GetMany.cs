using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Core.Entities.Base;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Base.Commands;

public record GetManyQuery<TResponse>
    : IRequest<IEnumerable<TResponse>>;

internal abstract class GetManyHandler<TQuery, TEntity, TResponse>
    : QueryHandler<TEntity>, IRequestHandler<TQuery, IEnumerable<TResponse>>
    where TQuery : GetManyQuery<TResponse>
    where TEntity : BaseEntity
{
    protected GetManyHandler(AppDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper) { }

    public async Task<IEnumerable<TResponse>> Handle(TQuery query, CancellationToken cancellationToken)
    {
        var entities = await GetEntities(query, cancellationToken);
        return CreateResponse(entities);
    }

    protected virtual async Task<IEnumerable<TEntity>> GetEntities(TQuery query, CancellationToken cancellationToken)
    {
        return await Repository.ToListAsync(cancellationToken);
    }

    protected virtual IEnumerable<TResponse> CreateResponse(IEnumerable<TEntity> entities)
    {
        return Mapper.Map<IEnumerable<TResponse>>(entities);
    }
}