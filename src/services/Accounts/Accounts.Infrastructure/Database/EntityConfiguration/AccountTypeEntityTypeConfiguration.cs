using Accounts.Domain.AggregatesModel.AccountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.Infrastructure.Database.EntityConfiguration
{
    public class AccountTypeEntityTypeConfiguration : IEntityTypeConfiguration<AccountType>
    {
        public void Configure(EntityTypeBuilder<AccountType> accountTypeConfiguration)
        {
            accountTypeConfiguration.HasKey(a => a.Id);

            accountTypeConfiguration.Property(a => a.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            accountTypeConfiguration.Property(a => a.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}