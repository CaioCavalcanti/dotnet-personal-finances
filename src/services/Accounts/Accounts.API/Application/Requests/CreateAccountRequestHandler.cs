using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Accounts.API.Application.Responses;
using Accounts.Domain.AggregatesModel.AccountAggregate;
using AutoMapper;
using MediatR;

namespace Accounts.API.Application.Requests
{
    public class CreateAccountRequestHandler : IRequestHandler<CreateAccountRequest, AccountResponse>
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;

        public CreateAccountRequestHandler([NotNull] IAccountRepository repository, [NotNull] IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AccountResponse> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            var account = new Account(
                request.Name,
                AccountType.FromDisplayName<AccountType>(request.Type).Id,
                request.Currency,
                request.InitialBalance
            );

            _repository.Add(account);
            
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AccountResponse>(account);
        }
    }
}