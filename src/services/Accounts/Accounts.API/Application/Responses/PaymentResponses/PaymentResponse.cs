using System;

namespace Accounts.API.Application.Responses.PaymentResponses
{
    public record PaymentResponse
    {
        public int id { get; init; } 
        public DateTime Date { get; init; }
        public string Type { get; init; }
        public string Method { get; init; }
        public string Counterparty { get; init; }
        public double Value { get; init; }
        public string Description { get; init; }
        public DateTime Created { get; init; }
    }
}