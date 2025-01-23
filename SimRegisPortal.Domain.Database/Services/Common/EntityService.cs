using System.Linq.Expressions;
using SimRegisPortal.Domain.Data.Entities.Common;
using SimRegisPortal.Domain.Database.Repository;
using SimRegisPortal.Domain.Database.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace SimRegisPortal.Domain.Database.Services.Common
{
    public class EntityService<TEntity> : IEntityService<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly IRepository<TEntity> Repository;
        protected readonly IUnitOfWork UnitOfWork;

        public EntityService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            Repository = unitOfWork.GetRepository<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false)
        {
            return await Repository.GetAllQuery(asNoTracking).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false)
        {
            return await Repository.GetAllQuery(asNoTracking).Where(predicate).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id, bool asNoTracking = false)
        {
            return await Repository.GetAllQuery(asNoTracking).FirstAsync(e => e.Id == id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await Repository.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Repository.AddRangeAsync(entities);
        }

        public async Task DeleteAsync(int id)
        {
            Repository.Delete(id);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            Repository.Delete(entity);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<int> ids)
        {
            Repository.DeleteRange(ids);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            Repository.DeleteRange(entities);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            Repository.Update(entity);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            Repository.UpdateRange(entities);
            await UnitOfWork.SaveChangesAsync();
        }
    }
}
