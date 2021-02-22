using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Domain.AggregatesModel.AccountAggregate;
using Accounts.Domain.AggregatesModel.PaymentAggregate;
using Accounts.Domain.SeedWork;
using Accounts.Infrastructure.Database;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Accounts.API.Infrastructure.DatabaseSeed
{
    public class AccountsDbContextSeed
    {
        private ILogger<AccountsDbContextSeed> _logger;
        private AccountsDbContext _dbContext;

        public AccountsDbContextSeed([NotNull] AccountsDbContext dbContext, [NotNull] ILogger<AccountsDbContextSeed> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task SeedAsync()
        {
            AsyncRetryPolicy retryPolicy = CreateRetryPolicy();

            await retryPolicy.ExecuteAsync(MigrateAndSeedDatabase);
        }

        private AsyncRetryPolicy CreateRetryPolicy()
        {
            var retries = 3;
            return Policy.Handle<SqlException>()
                .WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timespan, retry, context) =>
                    {
                        _logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}.", nameof(AccountsDbContextSeed), exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }

        private async Task MigrateAndSeedDatabase()
        {
            using (_dbContext)
            {
                _dbContext.Database.Migrate();

                SeedEnumeration(_dbContext.AccountTypes, AccountType.GetAll<AccountType>());
                SeedEnumeration(_dbContext.PaymentMethods, PaymentMethod.GetAll<PaymentMethod>());
                SeedEnumeration(_dbContext.PaymentTypes, PaymentType.GetAll<PaymentType>());

                await _dbContext.SaveChangesAsync();
            }
        }

        private void SeedEnumeration<TEnumeration>(DbSet<TEnumeration> values, IEnumerable<TEnumeration> valuesToSeed)
            where TEnumeration : Enumeration
        {
            if (!values.Any())
            {
                values.AddRange(valuesToSeed);
            }
        }
    }
}