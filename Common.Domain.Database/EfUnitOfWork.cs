using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Common.Domain.Data;

namespace Common.Domain.Database
{
    public class EfUnitOfWork
    {
        private readonly DbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public EfUnitOfWork(DbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : BaseEntity
        {
            return _serviceProvider.GetRequiredService<IRepository<TEntity>>();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
