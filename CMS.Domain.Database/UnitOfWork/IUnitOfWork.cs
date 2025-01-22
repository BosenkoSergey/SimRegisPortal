using CMS.Domain.Data.Entities.Common;
using CMS.Domain.Database.Repository;

namespace CMS.Domain.Database.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        Task SaveChangesAsync();
    }
}
