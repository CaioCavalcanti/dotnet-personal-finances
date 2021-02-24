using Accounts.Domain.AggregatesModel.PaymentAggregate;
using AutoMapper;

namespace Accounts.API.Application.Responses.PaymentResponses
{
    public class PaymentMappingProfile : Profile
    {
        public PaymentMappingProfile()
        {
            CreateMap<Payment, PaymentSummaryResponse>()
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.Type.Name))
                .ForMember(dest => dest.Method, opts => opts.MapFrom(src => src.Method.Name));

            CreateMap<Payment, PaymentResponse>()
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.Type.Name))
                .ForMember(dest => dest.Method, opts => opts.MapFrom(src => src.Method.Name));
        }
    }
}