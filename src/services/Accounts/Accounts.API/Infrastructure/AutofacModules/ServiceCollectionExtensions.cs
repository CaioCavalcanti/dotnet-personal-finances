using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;

namespace Accounts.API.Infrastructure.AutofacModules
{
    public static class ServiceCollectionExtensions
    {
        public static ContainerBuilder RegisterAutofacModules(this ContainerBuilder container)
        {
            container
                .RegisterAutoMapper(typeof(Program).Assembly)
                .RegisterModule(new MediatorModule())
                .RegisterModule(new ApplicationModule());

            return container;
        }
    }
}