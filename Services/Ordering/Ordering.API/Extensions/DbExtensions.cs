using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

            try
            {
                logger.LogInformation("Started db migration: {DbContextName}", typeof(TContext).Name);

                var retry = Policy.Handle<SqlException>()
                    .WaitAndRetry(
                        retryCount: 5,
                        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                        onRetry: (exception, timeSpan, retryCount, contextRetry) =>
                        {
                            logger.LogWarning(exception, "Retrying because of {Exception} {Span}", exception.Message, timeSpan);
                        });

                retry.Execute(() =>
                {
                    // apply migrations
                    context?.Database.Migrate();

                    // call seeder
                    seeder(context!, services);
                });

                logger.LogInformation("Completed db migration: {DbContextName}", typeof(TContext).Name);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database.");
                throw;
            }

            return host;
        }
    }
}
