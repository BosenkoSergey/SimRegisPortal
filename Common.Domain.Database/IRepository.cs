using System.Linq.Expressions;
using Common.Domain.Data;

namespace Common.Domain.Database
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false);
        Task<TEntity> GetByIdAsync(int id, bool asNoTracking = false);
        Task AddAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task DeleteAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
    }
}
