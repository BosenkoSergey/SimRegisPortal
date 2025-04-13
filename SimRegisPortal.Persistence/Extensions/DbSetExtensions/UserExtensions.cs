using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Persistence.Extensions.DbSetExtensions;

public static class UserExtensions
{
    public static async Task<User?> GetForAuth(
        this DbSet<User> dbSet,
        Expression<Func<User, bool>> filter,
        CancellationToken cancellationToken = default)
    {
        return await dbSet
            .Include(u => u.Permissions)
            .Where(filter)
            .Where(u => u.Status == UserStatus.Active)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
