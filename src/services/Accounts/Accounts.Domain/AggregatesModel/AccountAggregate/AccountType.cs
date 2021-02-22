using System.Collections.Generic;
using Accounts.Domain.SeedWork;

namespace Accounts.Domain.AggregatesModel.AccountAggregate
{
    public class AccountType : Enumeration
    {
        public readonly static AccountType Cash = new AccountType(1, nameof(Cash).ToLowerInvariant());
        public readonly static AccountType Savings = new AccountType(2, nameof(Savings).ToLowerInvariant());
        public readonly static AccountType Checking = new AccountType(3, nameof(Checking).ToLowerInvariant());
        public readonly static AccountType Retirement = new AccountType(4, nameof(Retirement).ToLowerInvariant());
        public readonly static AccountType Investments = new AccountType(5, nameof(Investments).ToLowerInvariant());

        public AccountType(int id, string name) : base(id, name)
        {
        }
    }
}