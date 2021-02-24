using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Accounts.API.Application.Responses.AccountResponses;
using Accounts.Domain.AggregatesModel.AccountAggregate;
using AutoMapper;

namespace Accounts.API.Application.Queries
{
    public class AccountQueries : IAccountQueries
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;

        public AccountQueries([NotNull] IAccountRepository repository, [NotNull] IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AccountResponse> GetAccountAsync(int id)
        {
            Account account = await _repository.GetAsync(id);
            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<IEnumerable<AccountSummaryResponse>> GetAccountsAsync()
        {
            IEnumerable<Account> accounts = await _repository.GetAsync();
            return _mapper.Map<IEnumerable<AccountSummaryResponse>>(accounts);
        }
    }
}