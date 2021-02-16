using System.Reflection;
using Accounts.API.Application.Behaviors;
using Accounts.API.Application.Requests;
using Accounts.API.Application.Validators;
using Autofac;
using FluentValidation;
using MediatR;

namespace Accounts.API.Infrastructure.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            RegisterRequests(builder);
            RegisterRequestValidators(builder);

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t =>
                {
                    object o;
                    return componentContext.TryResolve(t, out o) ? o : null;
                };
            });

            RegisterPipelineBehaviors(builder);
        }

        private void RegisterRequests(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(CreateAccountRequest).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));
        }

        private void RegisterRequestValidators(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(CreateAccountRequestValidator).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();
        }

        private void RegisterPipelineBehaviors(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}