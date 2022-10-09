using BuildingBlocks.Domain;
using Domain.Customers;
using Domain.Products;

namespace Domain.Receipts
{
    public class Receipt : EntityBase, IAggregateRoot
    {
        public Guid Id { get; }

        public Guid CustomerId { get; }
        public Customer Customer { get; } = null!;

        public Guid ProductId { get; }
        public Product Product { get; } = null!;

        public int Quantity { get; }

        public decimal UnitPrice { get; }

        private Receipt() => Id = Guid.NewGuid();

        public Receipt(Guid customerId, Guid productId, int quantity, decimal unitPrice)
        {
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
