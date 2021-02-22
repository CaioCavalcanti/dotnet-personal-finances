using Accounts.Domain.AggregatesModel.PaymentAggregate;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.Database.EntityConfiguration
{
    public class PaymentEntityTypeConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Payment> paymentConfiguration)
        {
            paymentConfiguration.HasKey(a => a.Id);
            paymentConfiguration.Property(a => a.Counterparty).IsRequired();
            paymentConfiguration.Property(a => a.Value).IsRequired();

            paymentConfiguration
                .Property<int>("_accountId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("AccountId")
                .IsRequired();
                
            paymentConfiguration
                .Property<int>("_paymentTypeId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("PaymentTypeId")
                .IsRequired();

            paymentConfiguration
                .HasOne(a => a.Type)
                .WithMany()
                .HasForeignKey("_paymentTypeId");
                
            paymentConfiguration
                .Property<int>("_paymentMethodId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("PaymentMethodId")
                .IsRequired();

            paymentConfiguration
                .HasOne(a => a.Method)
                .WithMany()
                .HasForeignKey("_paymentMethodId");
        }
    }
}