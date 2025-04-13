using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Persistence.Extensions.DbSetExtensions
{
    public static class UserExtensions
    {
        public static async Task<User?> GetForAuth(
            this DbSet<User> dbSet,
            Expression<Func<User, bool>> filter,
            CancellationToken cancellationToken = default)
        {
            return await dbSet
                .AsSplitQuery()
                .Include(u => u.Permissions)
                .Include(u => u.ProjectPermissions)
                .Where(filter)
                .Where(u => u.Status != UserStatus.Deleted)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
