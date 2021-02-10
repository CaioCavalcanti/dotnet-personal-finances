using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.API.Application.Responses;

namespace Accounts.API.Application.Queries
{
    public class AccountQueries : IAccountQueries
    {
        public Task<AccountResponse> GetAccountAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<AccountSummaryResponse>> GetAccountsAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}