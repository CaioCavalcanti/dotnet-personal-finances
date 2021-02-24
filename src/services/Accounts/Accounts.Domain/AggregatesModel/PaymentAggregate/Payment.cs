using System;
using Accounts.Domain.SeedWork;

namespace Accounts.Domain.AggregatesModel.PaymentAggregate
{
    public class Payment : Entity, IAggregateRoot
    {
        private int _accountId;
        private int _paymentMethodId;
        private int _paymentTypeId;

        protected Payment() { }

        public Payment(int accountId, int paymentMethodId, int paymentTypeId, DateTime date, string counterparty, double value, string description)
        {
            _accountId = accountId;
            _paymentMethodId = paymentMethodId;
            _paymentTypeId = paymentTypeId;
            Date = date;
            Counterparty = counterparty;
            Value = value;
            Description = description;
            Created = DateTime.UtcNow;
        }

        public DateTime Date { get; set; }
        public PaymentType Type { get; set; }
        public PaymentMethod Method { get; set; }
        public string Counterparty { get; set; }
        public double Value { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
    }
}