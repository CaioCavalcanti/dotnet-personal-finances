using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Domain.SeedWork;

namespace Accounts.Domain.AggregatesModel.PaymentAggregate
{
    public interface IPaymentRepository: IRepository<Payment>
    {
        Task<Payment> GetAsync(int id);
        Task<IEnumerable<Payment>> GetAsync();
        Payment Add(Payment payment);
        void Update(Payment account);
    }
}