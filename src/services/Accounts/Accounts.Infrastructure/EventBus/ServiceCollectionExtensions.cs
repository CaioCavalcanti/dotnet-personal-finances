using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Infrastructure.EventBus
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventBus([NotNull] this IServiceCollection services)
        {
            services.AddSingleton<IEventBus, DomainEventBus>();

            return services;
        }
    }
}