using SimRegisPortal.Domain.Data.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace SimRegisPortal.Domain.Database.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(DbContext context)
        {
            DbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAllQuery(bool asNoTracking = false)
        {
            return asNoTracking ? DbSet.AsNoTracking() : DbSet;
        }

        public async Task AddAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            await DbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            ArgumentNullException.ThrowIfNull(entities, nameof(entities));
            await DbSet.AddRangeAsync(entities);
        }

        public void Delete(int id)
        {
            DbSet.Remove(CreateInstance(id));
        }

        public void Delete(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            DbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<int> ids)
        {
            ArgumentNullException.ThrowIfNull(ids, nameof(ids));
            DbSet.RemoveRange(ids.Select(CreateInstance));
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            ArgumentNullException.ThrowIfNull(entities, nameof(entities));
            DbSet.RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            DbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            ArgumentNullException.ThrowIfNull(entities, nameof(entities));
            DbSet.UpdateRange(entities);
        }

        private static TEntity CreateInstance(int id)
        {
            var entity = Activator.CreateInstance<TEntity>();
            entity.Id = id;
            return entity;
        }
    }
}
