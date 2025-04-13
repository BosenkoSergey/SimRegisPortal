using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Core.Entities.Base;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Base.Commands;

internal abstract class QueryHandler<TEntity>
    where TEntity : BaseEntity
{
    protected readonly IQueryable<TEntity> Repository;
    protected readonly IMapper Mapper;

    protected QueryHandler(AppDbContext dbContext, IMapper mapper)
    {
        Repository = dbContext.Set<TEntity>().AsNoTracking();
        Mapper = mapper;
    }
}