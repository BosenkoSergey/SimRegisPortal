using Common.Domain.Data;

namespace Common.Domain.Database
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        Task SaveChangesAsync();
    }
}
