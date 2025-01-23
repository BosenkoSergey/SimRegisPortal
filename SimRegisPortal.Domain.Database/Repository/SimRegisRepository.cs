using SimRegisPortal.Domain.Data.Entities.Common;

namespace SimRegisPortal.Domain.Database.Repository
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
