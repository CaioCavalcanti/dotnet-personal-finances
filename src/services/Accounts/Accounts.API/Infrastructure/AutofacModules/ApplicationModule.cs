using Accounts.API.Application.Queries;
using Accounts.Domain.AggregatesModel.AccountAggregate;
using Accounts.Infrastructure.Repositories;
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

            builder.RegisterType<AccountRepository>()
                .As<IAccountRepository>()
                .InstancePerLifetimeScope();
        }
    }
}