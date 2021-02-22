using Accounts.Domain.SeedWork;

namespace Accounts.Domain.AggregatesModel.PaymentAggregate
{
    public class PaymentMethod : Enumeration
    {
        public static PaymentMethod Card = new PaymentMethod(1, nameof(Card).ToLowerInvariant());
        public static PaymentMethod Cash = new PaymentMethod(2, nameof(Cash).ToLowerInvariant());
        public static PaymentMethod Transfer = new PaymentMethod(3, nameof(Transfer).ToLowerInvariant());

        public PaymentMethod(int id, string name) : base(id, name)
        {
        }
    }
}