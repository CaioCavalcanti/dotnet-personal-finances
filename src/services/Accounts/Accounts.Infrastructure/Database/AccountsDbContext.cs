using System.Threading;
using System.Threading.Tasks;
using Accounts.Domain.AggregatesModel.AccountAggregate;
using Accounts.Domain.SeedWork;
using Accounts.Infrastructure.Database.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.Database
{
    public class AccountsDbContext : DbContext, IUnitOfWork
    {
        public AccountsDbContext(DbContextOptions<AccountsDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await SaveChangesAsync(cancellationToken);

            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountEntityTypeConfiguration());
        }
    }
}