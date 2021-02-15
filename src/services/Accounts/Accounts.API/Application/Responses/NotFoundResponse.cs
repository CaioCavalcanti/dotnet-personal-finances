namespace Accounts.API.Application.Responses
{
    public class NotFoundResponse<T> where T : class
    {
        public string Message { get; } = $"{typeof(T).Name} not found.";
    }
}