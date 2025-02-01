using SimRegisPortal.Database.Data.Entities.Common;

namespace SimRegisPortal.Database.Access.Repositories.Common
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
