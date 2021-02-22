using Accounts.Domain.SeedWork;

namespace Accounts.Domain.AggregatesModel.PaymentAggregate
{
    public class PaymentType : Enumeration
    {
        public static PaymentType Received = new PaymentType(1, nameof(Received).ToLowerInvariant());
        public static PaymentType Sent = new PaymentType(2, nameof(Sent).ToLowerInvariant());

        public PaymentType(int id, string name) : base(id, name)
        {
        }
    }
}