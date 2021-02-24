using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;

namespace Accounts.API.Infrastructure.AutofacModules
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterAutofacModules(this ContainerBuilder container)
        {
            container
                .RegisterAutoMapper(typeof(Program).Assembly)
                .RegisterModule(new MediatorModule())
                .RegisterModule(new ApplicationModule());

            return container;
        }

        public static ContainerBuilder RegisterAsScoped<TInterface, TType>(this ContainerBuilder container)
        {
            container.RegisterType<TType>()
                .As<TInterface>()
                .InstancePerLifetimeScope();

            return container;
        }
    }
}