using Accounts.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.Database.EntityConfiguration
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder ApplyEnumerationConfiguration<TEnumeration>(this ModelBuilder builder)
            where TEnumeration : Enumeration
        {
            builder.ApplyConfiguration(new EnumerationEntityTypeConfiguration<TEnumeration>());
            return builder;
        }
    }
}