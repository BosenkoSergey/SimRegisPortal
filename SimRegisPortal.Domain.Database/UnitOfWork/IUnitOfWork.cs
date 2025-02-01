using SimRegisPortal.Domain.Data.Entities.Common;
using SimRegisPortal.Domain.Database.Repositories.Common;

namespace SimRegisPortal.Domain.Database.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        Task SaveChangesAsync();
    }
}
