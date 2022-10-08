using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Domains.Customers
{
    internal class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.FullName);
            builder.Property(a => a.EMail);
            builder.Property(a => a.Cell);

            builder.HasIndex(a => a.EMail).IsUnique();
            builder.HasIndex(a => a.Cell).IsUnique();
        }
    }
}
