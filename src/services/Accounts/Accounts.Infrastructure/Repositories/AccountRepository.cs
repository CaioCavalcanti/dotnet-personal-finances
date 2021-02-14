using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Domain.AggregatesModel.AccountAggregate;
using Accounts.Domain.SeedWork;
using Accounts.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountsDbContext _context;

        public AccountRepository(AccountsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public Account Add(Account account)
        {
            return _context.Accounts.Add(account).Entity;
        }

        public Task<Account> GetAsync(int id)
        {
            return _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Account>> GetAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public void Update(Account account)
        {
            _context.Entry(account).State = EntityState.Modified;
        }
    }
}