using System.ComponentModel;
using System;
namespace Accounts.API.Application.Responses
{
    public class ErrorResponse
    {
        public ErrorResponse(string correlationId, string message)
        {
            CorrelationId = correlationId;
            Message = message;
        }

        public string Message { get; }
        public string CorrelationId { get; }
        public string Exception { get; private set; }
        public string InnerException { get; private set; }
        public string StackTrace { get; private set; }

        public void AddExceptionDetails(Exception exception)
        {
            Exception = $"{exception.GetType().Name}: {exception.Message}";
            StackTrace = exception.StackTrace;

            if (exception.InnerException != null)
            {
                InnerException = $"{exception.InnerException.GetType().Name}: {exception.InnerException.Message}";
            }
        }
    }
}