using CMS.Domain.Data.Entities.Common;

namespace CMS.Domain.Database.Repository
{
    public class CmsRepository<TEntity> : Repository<TEntity>
        where TEntity : BaseEntity
    {
        protected CmsRepository(CmsDbContext context)
            : base(context)
        {
        }
    }
}
