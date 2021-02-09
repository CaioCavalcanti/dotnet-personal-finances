using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Domain.SeedWork;

namespace Accounts.Domain.AggregatesModel.AccountAggregate
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetAsync(int id);
        Task<IEnumerable<Account>> GetAsync();
        Account Add(Account account);
        void Update(Account account);
    }
}