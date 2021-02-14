using System.Diagnostics.CodeAnalysis;
using Accounts.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Infrastructure.Database
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAccountsDbContext([NotNull] this IServiceCollection services, [NotNull] IConfiguration configuration)
        {
            services.AddDbContext<AccountsDbContext>(options => options.UseNpgsql(GetConnectionString(configuration)));
            return services;
        }

        private static string GetConnectionString(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(nameof(AccountsDbContext));

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InfrastructureException("Could not find database connection string.");
            }

            return connectionString;
        }
    }
}