using System;

namespace Accounts.API.Application.Responses
{
    public record AccountResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Type { get; init; }
        public double InitialBalance { get; init; }
        public DateTime CreatedAt { get; init; }
        public double Balance { get; init; }
    }
}