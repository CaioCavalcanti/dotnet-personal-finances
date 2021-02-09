namespace Accounts.API.Application.Responses
{
    public class AccountSummaryResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Type { get; init; }
        public double Balance { get; init; }
    }
}