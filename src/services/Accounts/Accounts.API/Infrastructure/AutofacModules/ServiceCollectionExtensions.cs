using Autofac;

namespace Accounts.API.Infrastructure.AutofacModules
{
    public static class ServiceCollectionExtensions
    {
        public static ContainerBuilder RegisterAutofacModules(this ContainerBuilder container)
        {
            container
                .RegisterModule(new MediatorModule())
                .RegisterModule(new ApplicationModule());

            return container;
        }
    }
}