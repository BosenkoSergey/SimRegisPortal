using SimRegisPortal.Domain.Entities.Common;

namespace SimRegisPortal.Persistence.Repositories.Common
{
    public class SimRegisRepository<TEntity> : Repository<TEntity>
        where TEntity : BaseEntity
    {
        protected SimRegisRepository(SimRegisDbContext context)
            : base(context)
        {
        }
    }
}
