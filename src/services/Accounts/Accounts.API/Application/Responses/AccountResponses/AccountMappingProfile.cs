using Accounts.Domain.AggregatesModel.AccountAggregate;
using AutoMapper;

namespace Accounts.API.Application.Responses.AccountResponses
{
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<Account, AccountSummaryResponse>()
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.Type.Name));

            CreateMap<Account, AccountResponse>()
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.Type.Name));
        }
    }
}