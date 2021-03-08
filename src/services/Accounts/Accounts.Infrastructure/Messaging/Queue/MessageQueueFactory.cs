using System.Diagnostics.CodeAnalysis;
using Accounts.Infrastructure.Exceptions;
using Azure.Storage.Queues;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Accounts.Infrastructure.Messaging.Queue
{
    public class MessageQueueFactory : IMessageQueueFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;

        public MessageQueueFactory([NotNull] IConfiguration configuration, [NotNull] IMemoryCache cache)
        {
            _configuration = configuration;
            _cache = cache;
        }

        public IMessageQueue Create(string queueName)
        {
            if (!_cache.TryGetValue(queueName, out QueueClient queueClient))
            {
                queueClient = CreateAzureStorageQueueClient(queueName);
                _cache.Set(queueName, queueClient);
            }
            return new AzureStorageQueue(queueClient);
        }

        private QueueClient CreateAzureStorageQueueClient(string queueName)
        {
            string connectionString = GetConnectionString(queueName);
            var client = new QueueClient(connectionString, queueName);

            client.CreateIfNotExists();

            return client;
        }

        private string GetConnectionString(string queueName)
        {
            string connectionStringKey = $"az-storage-queue-{queueName}";
            string connectionString = _configuration.GetConnectionString(connectionStringKey);

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InfrastructureException($"Could not find connection string for queue {queueName}.");
            }

            return connectionString;
        }
    }
}