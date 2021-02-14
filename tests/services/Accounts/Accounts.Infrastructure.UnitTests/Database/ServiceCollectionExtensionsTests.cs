using Moq;
using FluentAssertions;
using Xunit;
using Microsoft.Extensions.Configuration;
using System;
using Accounts.Infrastructure.Database;
using Accounts.Infrastructure.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Collections;

namespace Accounts.Infrastructure.UnitTests.Database
{
    public class ServiceCollectionExtensionsTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ThrowsInfrastructureException_WhenNoAccountsDbContextConnectionString(string connectionStringValue)
        {
            // arrange
            IServiceCollection services = Mock.Of<IServiceCollection>();
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string> {
                    { nameof(AccountsDbContext), connectionStringValue }
                })
                .Build();

            // act
            Func<IServiceCollection> act = () => services.AddAccountsDbContext(configuration);

            // arrange
            act.Should().Throw<InfrastructureException>()
                .WithMessage("Could not find database connection string.");
        }
    }
}