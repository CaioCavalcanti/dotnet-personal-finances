namespace Accounts.Domain.Exceptions
{
    [System.Serializable]
    public class AccountsDomainException : System.Exception
    {
        public AccountsDomainException() { }
        public AccountsDomainException(string message) : base(message) { }
        public AccountsDomainException(string message, System.Exception inner) : base(message, inner) { }
    }
}