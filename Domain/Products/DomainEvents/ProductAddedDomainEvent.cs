using BuildingBlocks.Domain.DomainEvents;

namespace Domain.Products.DomainEvents
{
    public class ProductAddedDomainEvent : DomainEventBase
    {
        public Product Product { get; }

        public ProductAddedDomainEvent(Product product)
        {
            Product = product;
        }
    }
}
