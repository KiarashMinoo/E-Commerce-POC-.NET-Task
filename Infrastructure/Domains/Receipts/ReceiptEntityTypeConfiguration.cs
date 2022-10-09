using Domain.Receipts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Domains.Receipts
{
    internal class ReceiptEntityTypeConfiguration : IEntityTypeConfiguration<Receipt>
    {
        public void Configure(EntityTypeBuilder<Receipt> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Quantity);
            builder.Property(a => a.UnitPrice);

            builder.HasOne(a => a.Customer).WithMany().HasForeignKey(a => a.CustomerId);
            builder.HasOne(a => a.Product).WithMany().HasForeignKey(a => a.ProductId);
        }
    }
}
