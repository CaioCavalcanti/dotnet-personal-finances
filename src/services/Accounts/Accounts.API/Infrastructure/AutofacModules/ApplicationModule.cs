using Accounts.API.Application.Queries;
using Accounts.Domain.AggregatesModel.AccountAggregate;
using Accounts.Domain.AggregatesModel.PaymentAggregate;
using Accounts.Infrastructure.Repositories;
using Autofac;

namespace Accounts.API.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAsScoped<IAccountQueries, AccountQueries>()
                .RegisterAsScoped<IAccountRepository, AccountRepository>()
                .RegisterAsScoped<IPaymentQueries, PaymentQueries>()
                .RegisterAsScoped<IPaymentRepository, PaymentRepository>();
        }
    }
}