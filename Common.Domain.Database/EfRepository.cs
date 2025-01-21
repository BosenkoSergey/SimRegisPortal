using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Common.Domain.Data;

namespace Common.Domain.Database
{
    public class EfRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly DbSet<TEntity> _dbSet;

        public EfRepository(DbContext context)
        {
            _dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false)
        {
            var query = asNoTracking ? _dbSet.AsNoTracking() : _dbSet;
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false)
        {
            var query = asNoTracking ? _dbSet.AsNoTracking() : _dbSet;
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id, bool asNoTracking = false)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }

            var query = asNoTracking ? _dbSet.AsNoTracking() : _dbSet;
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
            await _dbSet.AddAsync(entity);
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
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            _dbSet.Update(entity);
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
