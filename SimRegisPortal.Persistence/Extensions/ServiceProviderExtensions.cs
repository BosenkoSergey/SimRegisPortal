using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SimRegisPortal.Persistence.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void PrepareDatabase(this IServiceProvider source)
        {
            using var scope = source.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<SimRegisDbContext>();
            context.Database.Migrate();
            context.Seed();
        }
    }
}
