using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Accounts.API.Application.Responses.PaymentResponses;
using Accounts.Domain.AggregatesModel.PaymentAggregate;
using AutoMapper;

namespace Accounts.API.Application.Queries
{
    public class PaymentQueries : IPaymentQueries
    {
        private readonly IPaymentRepository _repository;
        private readonly IMapper _mapper;

        public PaymentQueries([NotNull] IPaymentRepository repository, [NotNull] IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaymentResponse> GetPaymentAsync(int id)
        {
            Payment payment = await _repository.GetAsync(id);
            return _mapper.Map<PaymentResponse>(payment);
        }

        public async Task<IEnumerable<PaymentSummaryResponse>> GetPaymentsAsync()
        {
            IEnumerable<Payment> payments = await _repository.GetAsync();
            return _mapper.Map<IEnumerable<PaymentSummaryResponse>>(payments);
        }
    }
}