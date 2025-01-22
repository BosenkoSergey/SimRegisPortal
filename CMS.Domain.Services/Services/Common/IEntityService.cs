using System.Linq.Expressions;
using CMS.Domain.Data.Entities.Common;

namespace CMS.Domain.Workflow.Services.Common
{
    public interface IEntityService<TEntity>
        where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false);
        Task<TEntity> GetByIdAsync(int id, bool asNoTracking = false);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(int id);
        Task DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(IEnumerable<int> ids);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
    }
}
