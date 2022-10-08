using BuildingBlocks.Domain;
using Domain.Customers;
using Domain.Products;

namespace Domain.Receipts
{
    public class Receipt : EntityBase, IAggregateRoot
    {
        public Guid CustomerId { get; }
        public Customer Customer { get; } = null!;

        public Guid ProductId { get; }
        public Product Product { get; } = null!;

        public int Quantity { get; }

        private Receipt()
        {
        }

        public Receipt(Guid customerId, Guid productId, int quantity)
        {
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
