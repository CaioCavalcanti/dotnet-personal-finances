using Accounts.Domain.AggregatesModel.AccountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.Infrastructure.Database.EntityConfiguration
{
    public class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> accountConfiguration)
        {
            accountConfiguration.HasKey(a => a.Id);
            accountConfiguration.Property(a => a.Currency).IsRequired();
            accountConfiguration.Property(a => a.Name).IsRequired();

            accountConfiguration
                .Property<int>("_accountTypeId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("AccountTypeId")
                .IsRequired();

            accountConfiguration
                .HasOne(a => a.Type)
                .WithMany()
                .HasForeignKey("_accountTypeId");
        }
    }
}