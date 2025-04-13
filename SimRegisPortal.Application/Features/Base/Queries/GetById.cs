using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Core.Entities.Base;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Base.Queries;

public record GetByIdQuery<TKey, TResponse>(TKey Id)
    : GetOneQuery<TResponse>;

internal abstract class GetByIdHandler<TQuery, TEntity, TKey, TResponse>(AppDbContext dbContext, IMapper mapper)
    : GetOneHandler<TQuery, TEntity, TResponse>(dbContext, mapper)
    where TQuery : GetByIdQuery<TKey, TResponse>
    where TEntity : BaseEntity<TKey>
{
    protected override async Task<TEntity> GetEntity(TQuery query, CancellationToken cancellationToken)
    {
        return await GetEntitiesQuery()
                .Where(entity => entity.Id!.Equals(query.Id))
                .FirstOrDefaultAsync(cancellationToken)
            ?? throw new ResourceNotFoundException(typeof(TEntity).Name);
    }
}