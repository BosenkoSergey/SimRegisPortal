using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Persistence.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void PrepareDatabase(this IServiceProvider source)
        {
            using var scope = source.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
            context.Seed();
        }
    }
}
