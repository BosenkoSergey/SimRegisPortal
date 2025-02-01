using SimRegisPortal.Domain.Data.Entities.Common;

namespace SimRegisPortal.Domain.Database.Repositories.Common
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAllQuery(bool asNoTracking = false);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Delete(int id);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<int> ids);
        void DeleteRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
    }
}
