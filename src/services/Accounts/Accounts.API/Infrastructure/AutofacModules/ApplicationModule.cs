using System.Collections.Immutable;
using Accounts.API.Application.Queries;
using Autofac;

namespace Accounts.API.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountQueries>()
                .As<IAccountQueries>()
                .InstancePerLifetimeScope();
        }
    }
}