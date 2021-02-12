using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Accounts.Infrastructure.Database
{
    public class AccountsDbContextDesignFactory : IDesignTimeDbContextFactory<AccountsDbContext>
    {
        public AccountsDbContext CreateDbContext(string[] args)
        {
            IConfiguration config = GetConfiguration();
            
            var optionsBuilder = new DbContextOptionsBuilder<AccountsDbContext>()
                .UseNpgsql(config.GetConnectionString(nameof(AccountsDbContext)));

            return new AccountsDbContext(optionsBuilder.Options);
        }

        private IConfiguration GetConfiguration()
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Accounts.API"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}