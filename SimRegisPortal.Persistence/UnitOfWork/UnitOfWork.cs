using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimRegisPortal.Domain.Entities.Common;
using SimRegisPortal.Persistence.Repositories.Common;

namespace SimRegisPortal.Persistence.UnitOfWork
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly IServiceProvider _serviceProvider;

        protected UnitOfWork(DbContext context, IServiceProvider serviceProvider)
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
