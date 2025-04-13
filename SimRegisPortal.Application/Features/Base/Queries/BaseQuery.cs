using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Core.Entities.Base;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Base.Queries;

internal abstract class QueryHandler<TEntity>(AppDbContext dbContext, IMapper mapper)
    where TEntity : BaseEntity
{
    protected readonly IQueryable<TEntity> Repository = dbContext.Set<TEntity>().AsNoTracking();
    protected readonly IMapper Mapper = mapper;

    protected virtual IQueryable<TEntity> GetEntitiesQuery()
    {
        return Repository;
    }
}