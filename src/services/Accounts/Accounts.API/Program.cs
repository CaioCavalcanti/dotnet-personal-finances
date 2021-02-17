using Accounts.API.Infrastructure.DatabaseSeed;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Accounts.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHost(args)
                .MigrateAccountsDbContext()
                .Run();
        }

        public static IWebHost CreateWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
