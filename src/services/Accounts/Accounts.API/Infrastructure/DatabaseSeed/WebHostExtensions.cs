using System.Text.RegularExpressions;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Microsoft.Data.SqlClient;
using Polly.Retry;
using Accounts.Infrastructure.Database;

namespace Accounts.API.Infrastructure.DatabaseSeed
{
    public static class WebHostExtensions
    {
        public static IWebHost MigrateAccountsDbContext(this IWebHost host)
        {
            host.MigrateDbContext<AccountsDbContext>((dbContext, services) =>
            {
                var logger = services.GetService<ILogger<AccountsDbContextSeed>>();

                new AccountsDbContextSeed(dbContext, logger)
                    .SeedAsync()
                    .Wait();
            });

            return host;
        }

        private static IWebHost MigrateDbContext<TDbContext>(this IWebHost webHost, Action<TDbContext, IServiceProvider> seeder)
            where TDbContext : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TDbContext>>();
                var dbContext = services.GetService<TDbContext>();

                try
                {
                    logger.LogInformation("Migrating database associated with context {DbContextName}.", typeof(TDbContext));

                    RetryPolicy retry = CreateRetryPolicy<TDbContext>(logger);

                    retry.Execute(() => InvokeSeeder(seeder, dbContext, services));
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}.", typeof(TDbContext).Name);
                }
            }

            return webHost;
        }

        private static RetryPolicy CreateRetryPolicy<TDbContext>(ILogger logger)
            where TDbContext : DbContext
        {
            var retries = 10;
            return Policy.Handle<SqlException>()
                .WaitAndRetry(
                    retryCount: retries,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, timespan, retry, context) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}.", nameof(TDbContext), exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services)
            where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}