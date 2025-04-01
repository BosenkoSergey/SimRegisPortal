using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Domain.Entities;
using SimRegisPortal.Domain.Enums;

namespace SimRegisPortal.Persistence.Extensions
{
    public static class UsersExtensions
    {
        public static async Task<User?> GetForAuth(
            this DbSet<User> dbSet,
            Expression<Func<User, bool>> filter,
            CancellationToken cancellationToken = default)
        {
            return await dbSet
                .AsSplitQuery()
                .Where(filter)
                .Where(u => u.Status != UserStatus.Deleted)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
