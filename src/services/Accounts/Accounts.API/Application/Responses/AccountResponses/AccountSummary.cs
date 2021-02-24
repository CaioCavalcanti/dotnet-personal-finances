namespace Accounts.API.Application.Responses.AccountResponses
{
    public record AccountSummaryResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Type { get; init; }
        public string Currency { get; set; }
        public double Balance { get; init; }
    }
}