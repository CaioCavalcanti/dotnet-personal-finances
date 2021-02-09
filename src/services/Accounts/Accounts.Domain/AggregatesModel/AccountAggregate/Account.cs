using System;
using Accounts.Domain.SeedWork;

namespace Accounts.Domain.AggregatesModel.AccountAggregate
{
    public class Account : Entity, IAggregateRoot
    {
        public Account(string name, AccountType type, double initialBalance)
        {
            Name = name;
            Type = type;
            InitialBalance = initialBalance;
            Balance = InitialBalance;
            CreatedAt = DateTime.UtcNow;
        }

        public string Name { get; }
        public AccountType Type { get; }
        public double InitialBalance { get; }
        public DateTime CreatedAt { get; }
        public double Balance { get; private set; }
    }
}