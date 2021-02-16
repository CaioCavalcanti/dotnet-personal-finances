using Accounts.API.Application.Responses;
using Accounts.Domain.AggregatesModel.AccountAggregate;
using MediatR;

namespace Accounts.API.Application.Requests
{
    public class CreateAccountRequest : IRequest<AccountResponse>
    {
        public string Name { get; set; }
        public AccountType Type { get; set; }
        public string Currency { get; set; }
        public double InitialBalance { get; set; }
    }
}