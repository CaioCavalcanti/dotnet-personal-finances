using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Accounts.Domain.AggregatesModel.AccountAggregate;
using Accounts.Domain.AggregatesModel.PaymentAggregate;
using Accounts.Domain.SeedWork;
using Accounts.Infrastructure.Database.EntityConfiguration;
using Accounts.Infrastructure.EventBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Accounts.Infrastructure.Database
{
    public class AccountsDbContext : DbContext, IUnitOfWork
    {
        private readonly IEventBus _eventBus;

        public AccountsDbContext(DbContextOptions<AccountsDbContext> options, IEventBus eventBus) : base(options)
        {
            _eventBus = eventBus;
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvents();

            await SaveChangesAsync(cancellationToken);

            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new AccountEntityTypeConfiguration())
                .ApplyEnumerationConfiguration<AccountType>()
                .ApplyConfiguration(new PaymentEntityTypeConfiguration())
                .ApplyEnumerationConfiguration<PaymentMethod>()
                .ApplyEnumerationConfiguration<PaymentType>();
        }

        private async Task DispatchDomainEvents()
        {
            IEnumerable<EntityEntry<Entity>> domainEntities = GetEntitiesWithDomainEventsToDispatch();
            IEnumerable<IDomainEvent> domainEvents = GetDomainEventsToDispatch(domainEntities);

            ClearDomainEvents(domainEntities);
            await _eventBus.DispatchDomainEvents(domainEvents);
        }

        private IEnumerable<EntityEntry<Entity>> GetEntitiesWithDomainEventsToDispatch()
        {
            return ChangeTracker
                .Entries<Entity>()
                .Where(e => e.Entity.DomainEvents != null && e.Entity.DomainEvents.Any());
        }

        private IEnumerable<IDomainEvent> GetDomainEventsToDispatch(IEnumerable<EntityEntry<Entity>> domainEntities)
        {
            return domainEntities
                .SelectMany(e => e.Entity.DomainEvents)
                .AsEnumerable();
        }

        private void ClearDomainEvents(IEnumerable<EntityEntry<Entity>> domainEntities)
        {
            foreach (var entity in domainEntities.Select(e => e.Entity))
            {
                entity.ClearDomainEvents();
            }
        }
    }
}