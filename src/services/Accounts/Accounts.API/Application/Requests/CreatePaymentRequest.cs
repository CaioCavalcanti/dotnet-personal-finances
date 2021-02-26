using System;
using Accounts.API.Application.Responses.PaymentResponses;
using MediatR;

namespace Accounts.API.Application.Requests
{
    public class CreatePaymentRequest : IRequest<PaymentResponse>
    {
        public int AccountId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Method { get; set; }
        public string Counterparty { get; set; }
        public double Value { get; set; }
        public string Description { get; set; }
    }
}