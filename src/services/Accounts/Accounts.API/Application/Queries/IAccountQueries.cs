using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.API.Application.Responses;

namespace Accounts.API.Application.Queries
{
    public interface IAccountQueries
    {
         Task<AccountResponse> GetAccountAsync(int id);
         Task<IEnumerable<AccountSummaryResponse>> GetAccountsAsync();

    }
}