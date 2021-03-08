using System;
using Accounts.Domain.SeedWork;

namespace Accounts.Domain.Events
{
    public class AccountCreatedDomainEvent : IDomainEvent
    {
        public AccountCreatedDomainEvent(string name, int typeId, string currency, double initialBalance, double balance, DateTime created)
        {
            Name = name;
            TypeId = typeId;
            Currency = currency;
            InitialBalance = initialBalance;
            Balance = balance;
            Created = created;
        }

        public string Name { get; }
        public int TypeId { get; }
        public string Currency { get; }
        public double InitialBalance { get; }
        public double Balance { get; private set; }
        public DateTime Created { get; }
    }
}