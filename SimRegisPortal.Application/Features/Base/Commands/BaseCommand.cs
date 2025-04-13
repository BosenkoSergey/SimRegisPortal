using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Core.Entities.Base;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Base.Commands;

internal abstract class CommandHandler<TEntity>(AppDbContext dbContext)
    where TEntity : BaseEntity
{
    protected readonly AppDbContext DbContext = dbContext;
    protected readonly DbSet<TEntity> DbSet = dbContext.Set<TEntity>();
}