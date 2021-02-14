using Xunit;
using AutoMapper;
using Accounts.API.Application.Responses;

namespace Accounts.API.UnitTests.Application.Responses
{
    public class AccountMappingProfileTests
    {
        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AccountMappingProfile>();
            });

            config.AssertConfigurationIsValid();
        }
    }
}