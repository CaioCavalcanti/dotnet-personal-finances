using System;

namespace Accounts.API.Application.Responses.AccountResponses
{
    public class AccountResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Currency { get; set; }
        public double InitialBalance { get; set; }
        public double Balance { get; set; }
        public DateTime Created { get; set; }
    }
}