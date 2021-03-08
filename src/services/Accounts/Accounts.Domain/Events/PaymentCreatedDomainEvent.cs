using System;
using Accounts.Domain.SeedWork;

namespace Accounts.Domain.Events
{
    public class PaymentCreatedDomainEvent : IDomainEvent
    {
        public PaymentCreatedDomainEvent(int accountId, DateTime date, int typeId, int methodId, string counterparty, double value, string description, DateTime created)
        {
            AccountId = accountId;
            Date = date;
            TypeId = typeId;
            MethodId = methodId;
            Counterparty = counterparty;
            Value = value;
            Description = description;
            Created = created;
        }

        public int AccountId { get; }
        public DateTime Date { get; }
        public int TypeId { get; }
        public int MethodId { get; }
        public string Counterparty { get; }
        public double Value { get; }
        public string Description { get; }
        public DateTime Created { get; }
    }
}