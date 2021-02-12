using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Infrastructure.Database
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAccountsDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AccountsDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString(nameof(AccountsDbContext)))
            );
            return services;
        }
    }
}