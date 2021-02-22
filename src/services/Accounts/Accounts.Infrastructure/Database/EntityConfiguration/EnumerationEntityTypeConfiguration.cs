using Accounts.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.Infrastructure.Database.EntityConfiguration
{
    public class EnumerationEntityTypeConfiguration<TEnumeration> : IEntityTypeConfiguration<TEnumeration>
        where TEnumeration : Enumeration
    {
        public void Configure(EntityTypeBuilder<TEnumeration> enumerationConfiguration)
        {
            enumerationConfiguration.HasKey(a => a.Id);

            enumerationConfiguration.Property(a => a.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            enumerationConfiguration.Property(a => a.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}