using Accounts.Domain.AggregatesModel.AccountAggregate;
using AutoMapper;

namespace Accounts.API.Application.Responses
{
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<Account, AccountSummaryResponse>();
            CreateMap<Account, AccountResponse>();
        }
    }
}