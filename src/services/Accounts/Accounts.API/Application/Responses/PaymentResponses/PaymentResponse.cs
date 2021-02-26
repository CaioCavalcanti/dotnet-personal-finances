using System;

namespace Accounts.API.Application.Responses.PaymentResponses
{
    public class PaymentResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Method { get; set; }
        public string Counterparty { get; set; }
        public double Value { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
    }
}