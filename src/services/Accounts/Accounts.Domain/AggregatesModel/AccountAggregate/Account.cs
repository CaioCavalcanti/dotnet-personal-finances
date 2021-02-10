using System;
using Accounts.Domain.SeedWork;

namespace Accounts.Domain.AggregatesModel.AccountAggregate
{
    public class Account : Entity, IAggregateRoot
    {
        public Account(string name, AccountType type, string currency, double initialBalance)
        {
            Name = name;
            Type = type;
            Currency = currency;
            InitialBalance = initialBalance;
            Balance = InitialBalance;
            CreatedAt = DateTime.UtcNow;
        }

        public string Name { get; }
        public AccountType Type { get; }
        public string Currency { get; }
        public double InitialBalance { get; }
        public DateTime CreatedAt { get; }
        public double Balance { get; private set; }
    }
}