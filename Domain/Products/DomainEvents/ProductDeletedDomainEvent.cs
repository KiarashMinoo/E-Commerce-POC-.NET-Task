using BuildingBlocks.Domain.DomainEvents;

namespace Domain.Products.DomainEvents
{
    public class ProductDeletedDomainEvent : DomainEventBase
    {
        public Product Product { get; }

        public ProductDeletedDomainEvent(Product customer) => Product = customer;
    }
}
