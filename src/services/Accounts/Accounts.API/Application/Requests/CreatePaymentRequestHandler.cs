using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Accounts.API.Application.Responses.PaymentResponses;
using Accounts.Domain.AggregatesModel.PaymentAggregate;
using AutoMapper;
using MediatR;

namespace Accounts.API.Application.Requests
{
    public class CreatePaymentRequestHandler : IRequestHandler<CreatePaymentRequest, PaymentResponse>
    {
        private readonly IPaymentRepository _repository;
        private readonly IMapper _mapper;

        public CreatePaymentRequestHandler([NotNull] IPaymentRepository repository, [NotNull] IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaymentResponse> Handle(CreatePaymentRequest request, CancellationToken cancellationToken)
        {
            // FIXME: ideally we wouldn't need to do this, but there's a limitation 
            // with EF core and this enumeration approach, since the entities are not
            // attached to the context, it will try to add when we save.
            // https://stackoverflow.com/a/39165051
            PaymentMethod paymentMethod = PaymentMethod.FromDisplayName<PaymentMethod>(request.Method);
            PaymentType paymentType = PaymentType.FromDisplayName<PaymentType>(request.Type);

            var payment = new Payment(
                request.AccountId,
                paymentMethod.Id,
                paymentType.Id,
                request.Date,
                request.Counterparty,
                request.Value,
                request.Description
            );

            _repository.Add(payment);
            
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return _mapper.Map<PaymentResponse>(payment, options => 
                options.AfterMap((src, dest) => {
                    dest.Method = paymentMethod.Name;
                    dest.Type = paymentType.Name;
                }));
        }
    }
}