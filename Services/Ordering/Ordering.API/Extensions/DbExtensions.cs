using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using Microsoft.Data.SqlClient;

namespace Ordering.API.Extensions
{
    public static class DbExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder)
            where TContext : DbContext
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetService<TContext>();

            if (context == null)
            {
                logger.LogWarning("DbContext {DbContextName} is not registered in the container.", typeof(TContext).Name);
                return host;
            }

            try
            {
                logger.LogInformation("Started db migration: {DbContextName}", typeof(TContext).Name);

                var jitterer = new Random();

                var retry = Policy.Handle<SqlException>()
                    .WaitAndRetryAsync(
                        retryCount: 5,
                        sleepDurationProvider: retryAttempt =>
                        {
                            // exponential backoff with jitter
                            var delay = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                            var jitter = TimeSpan.FromMilliseconds(jitterer.Next(0, 100));
                            return delay + jitter;
                        },
                        onRetry: (exception, timeSpan, retryCount, contextRetry) =>
                        {
                            logger.LogWarning(exception, "Retrying database migration because of: {Exception} (attempt {Attempt})", exception.Message, retryCount);
                        });

                // Execute async migration and seeder, block to keep method synchronous
                retry.ExecuteAsync(async () =>
                {
                    await context.Database.MigrateAsync();

                    // seeder is synchronous; call directly
                    seeder(context, services);
                }).GetAwaiter().GetResult();

                logger.LogInformation("Completed db migration: {DbContextName}", typeof(TContext).Name);
            }
            catch (Exception ex)
            {
                // Log the failure but do not rethrow to avoid crashing the whole host.
                logger.LogError(ex, "An error occurred while migrating the database {DbContextName}. Application will continue to run, but database may not be initialized.", typeof(TContext).Name);
            }

            return host;
        }
    }
}
