using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Azure.Storage.Queues;

namespace Accounts.Infrastructure.Messaging.Queue
{
    internal class AzureStorageQueue : IMessageQueue
    {
        private readonly QueueClient _queueClient;

        public AzureStorageQueue([NotNull] QueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        public Task Send(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Message cannot be null or empty.", nameof(message));
            }

            return _queueClient.SendMessageAsync(message);
        }
    }
}