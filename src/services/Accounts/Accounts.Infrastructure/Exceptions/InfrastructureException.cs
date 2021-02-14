namespace Accounts.Infrastructure.Exceptions
{
    [System.Serializable]
    public class InfrastructureException : System.Exception
    {
        public InfrastructureException() { }
        public InfrastructureException(string message) : base(message) { }
        public InfrastructureException(string message, System.Exception inner) : base(message, inner) { }
        protected InfrastructureException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}