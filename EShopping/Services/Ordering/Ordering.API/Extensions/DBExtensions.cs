using Microsoft.EntityFrameworkCore;

namespace Ordering.API.Extensions
{
    public static class DBExtensions
    {
        public static IHost MigrateDatabase<TContext>
            (this IHost host, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation($"Migrating database associated with context {typeof(TContext).Name}");
                   RunSeed(context, seeder, services);
                    logger.LogInformation($"Migrated database associated with context {typeof(TContext).Name}");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred while migrating the database used on context {typeof(TContext).Name}");
                }
            }

            return host;


        }

        private static void RunSeed<TContext>(TContext? context, Action<TContext, IServiceProvider> seeder, IServiceProvider services) where TContext : DbContext
        {
            context?.Database.Migrate();
            seeder(context, services);
            
        }
    }
}
