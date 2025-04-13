using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Core.Entities.Base;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Base.Commands;

internal abstract class CommandHandler<TEntity>
    where TEntity : BaseEntity
{
    protected readonly AppDbContext DbContext;
    protected readonly DbSet<TEntity> DbSet;

    protected CommandHandler(AppDbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<TEntity>();
    }
}