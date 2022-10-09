using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Domains.Products
{
    internal class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name);
            builder.Property(a => a.Quantity);
            builder.Property(a => a.Price);

            builder.HasIndex(a => a.Name).IsUnique();
        }
    }
}
