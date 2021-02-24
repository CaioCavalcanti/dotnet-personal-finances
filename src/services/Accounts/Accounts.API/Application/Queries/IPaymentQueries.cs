using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.API.Application.Responses.PaymentResponses;

namespace Accounts.API.Application.Queries
{
    public interface IPaymentQueries
    {
        Task<PaymentResponse> GetPaymentAsync(int id);
        Task<IEnumerable<PaymentSummaryResponse>> GetPaymentsAsync();
    }
}