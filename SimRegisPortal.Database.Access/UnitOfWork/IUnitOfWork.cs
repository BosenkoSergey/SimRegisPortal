using SimRegisPortal.Database.Data.Entities.Common;
using SimRegisPortal.Database.Access.Repositories.Common;

namespace SimRegisPortal.Database.Access.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        Task SaveChangesAsync();
    }
}
