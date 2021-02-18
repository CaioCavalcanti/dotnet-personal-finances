using Accounts.API.Application.Responses;
using MediatR;

namespace Accounts.API.Application.Requests
{
    public class CreateAccountRequest : IRequest<AccountResponse>
    {
        public string Name { get; set; }
        // TODO: can we make swagger show the available options from predefined AccountTypes?
        public string Type { get; set; }
        public string Currency { get; set; }
        public double InitialBalance { get; set; }
    }
}