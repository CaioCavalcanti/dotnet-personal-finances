using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Accounts.API.Application.Responses;
using Accounts.Domain.AggregatesModel.AccountAggregate;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Accounts.API.Application.Requests
{
    public class CreateAccountRequest : IRequest<AccountResponse>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Currency { get; set; }
        public double InitialBalance { get; set; }
    }
}