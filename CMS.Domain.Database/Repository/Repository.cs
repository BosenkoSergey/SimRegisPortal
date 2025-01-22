using System.Linq.Expressions;
using CMS.Domain.Data.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Domain.Database.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(DbContext context)
        {
            DbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false)
        {
            var query = asNoTracking ? DbSet.AsNoTracking() : DbSet;
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false)
        {
            var query = asNoTracking ? DbSet.AsNoTracking() : DbSet;
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id, bool asNoTracking = false)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }

            var query = asNoTracking ? DbSet.AsNoTracking() : DbSet;
            var entity = await query.FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity of type {typeof(TEntity).Name} with ID {id} was not found.");
            }

            return entity;
        }

        public async Task AddAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            await DbSet.AddAsync(entity);
        }

        public Task DeleteAsync(int id)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }

            var entity = CreateInstance(id);
            return DeleteAsync(entity);
        }

        public Task DeleteAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            if (entity.Id < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(entity.Id), "ID must be greater than zero.");
            }
            DbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            DbSet.Update(entity);
            return Task.CompletedTask;
        }

        private static TEntity CreateInstance(int id)
        {
            var entity = Activator.CreateInstance<TEntity>();
            entity.Id = id;
            return entity;
        }
    }
}
