using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Core.Entities.Base;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Base.Commands;

public record GetByIdQuery<TKey, TResponse>(TKey Id)
    : GetOneQuery<TResponse>;

internal abstract class GetByIdHandler<TQuery, TEntity, TKey, TResponse>
    : GetOneHandler<TQuery, TEntity, TResponse>
    where TQuery : GetByIdQuery<TKey, TResponse>
    where TEntity : BaseEntity<TKey>
{
    protected GetByIdHandler(AppDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper) { }

    protected override async Task<TEntity> GetEntity(TQuery query, CancellationToken cancellationToken)
    {
        return await Repository
                .Where(entity => entity.Id!.Equals(query.Id))
                .FirstOrDefaultAsync(cancellationToken)
            ?? throw new ResourceNotFoundException(typeof(TEntity).Name);
    }
}