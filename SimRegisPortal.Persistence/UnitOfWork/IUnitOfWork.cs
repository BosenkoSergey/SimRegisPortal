using SimRegisPortal.Domain.Entities.Common;
using SimRegisPortal.Persistence.Repositories.Common;

namespace SimRegisPortal.Persistence.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        Task SaveChangesAsync();
    }
}
