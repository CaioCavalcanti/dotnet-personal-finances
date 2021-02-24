using Xunit;
using AutoMapper;
using Accounts.Domain.AggregatesModel.AccountAggregate;
using FluentAssertions;
using Accounts.API.Application.Responses.AccountResponses;

namespace Accounts.API.UnitTests.Application.Responses
{
    public class AccountMappingProfileTests
    {
        private readonly IMapper _mapper;

        public AccountMappingProfileTests()
        {
            _mapper = new MapperConfiguration(cfg => { cfg.AddProfile<AccountMappingProfile>(); })
                .CreateMapper();
        }

        [Fact]
        public void ConfigurationIsValid()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AccountMappingProfile>();
            });

            config.AssertConfigurationIsValid();
        }

        [Fact]
        public void OnAccountToAccountSummaryResponse_MapTypeToString()
        {
            // arrange
            var account = new Account("Savings", AccountType.Savings.Id, "USD", 0);
            // act
            var response = _mapper.Map<AccountSummaryResponse>(account);
            // assert
            response.Type.Should().Be(AccountType.Savings.Name);
        }

        [Fact]
        public void OnAccountToAccountResponse_MapTypeToString()
        {
            // arrange
            var account = new Account("Savings", AccountType.Savings.Id, "USD", 0);
            // act
            var response = _mapper.Map<AccountResponse>(account);
            // assert
            response.Type.Should().Be(AccountType.Savings.Name);
        }
    }
}