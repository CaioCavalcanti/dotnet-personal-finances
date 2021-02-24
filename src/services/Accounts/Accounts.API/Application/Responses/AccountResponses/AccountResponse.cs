using System;

namespace Accounts.API.Application.Responses.AccountResponses
{
    public record AccountResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Type { get; init; }
        public string Currency { get; init; }
        public double InitialBalance { get; init; }
        public double Balance { get; init; }
        public DateTime Created { get; init; }
    }
}