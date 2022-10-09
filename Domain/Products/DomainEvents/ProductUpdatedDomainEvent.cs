using BuildingBlocks.Domain.DomainEvents;

namespace Domain.Products.DomainEvents
{
    public class ProductUpdatedDomainEvent : DomainEventBase
    {
        public Product Product { get; }

        public ProductUpdatedDomainEvent(Product customer) => Product = customer;
    }
}
