using System;

namespace Accounts.API.Application.Responses.PaymentResponses
{
    public record PaymentSummaryResponse
    {
        public int id { get; init; } 
        public DateTime Date { get; init; }
        public string Type { get; init; }
        public string Method { get; init; }
        public string Counterparty { get; init; }
        public double Value { get; init; }
    }
}