using System;
using Accounts.Domain.SeedWork;

namespace Accounts.Domain.AggregatesModel.AccountAggregate
{
    public class Account : Entity, IAggregateRoot
    {
        private int _accountTypeId;

        protected Account() { }

        public Account(string name, int typeId, string currency, double initialBalance)
        {
            Name = name;
            _accountTypeId = typeId;
            Currency = currency;
            InitialBalance = initialBalance;
            Balance = InitialBalance;
            Created = DateTime.UtcNow;
        }

        public string Name { get; }
        public virtual AccountType Type { get; }
        // TODO: convert into enumeration
        public string Currency { get; }
        public double InitialBalance { get; }
        public double Balance { get; private set; }
        public DateTime Created { get; }
    }
}