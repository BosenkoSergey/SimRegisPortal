using MediatR;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Base.Queries;

public abstract record GetLookupQuery<TKey>
    : IRequest<Dictionary<TKey, string>>
    where TKey : struct;

internal abstract class GetLookupHandler<TQuery, TEntity, TKey>(AppDbContext DbContext)
    : IRequestHandler<TQuery, Dictionary<TKey, string>>
    where TQuery : GetLookupQuery<TKey>
    where TEntity : BaseEntity
    where TKey : struct
{
    protected readonly IQueryable<TEntity> Repository = DbContext.Set<TEntity>().AsNoTracking();

    public async Task<Dictionary<TKey, string>> Handle(TQuery query, CancellationToken cancellationToken)
    {
        return await GetLookupEntities();
    }

    protected abstract Task<Dictionary<TKey, string>> GetLookupEntities();
}