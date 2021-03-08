using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Infrastructure.Messaging.Queue
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMessageQueue([NotNull] this IServiceCollection services)
        {
            services.AddSingleton<IMessageQueueFactory, MessageQueueFactory>();

            return services;
        }
    }
}