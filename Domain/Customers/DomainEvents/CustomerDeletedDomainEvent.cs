using BuildingBlocks.Domain.DomainEvents;

namespace Domain.Customers.DomainEvents
{
    public class CustomerDeletedDomainEvent : DomainEventBase
    {
        public Customer Customer { get; }

        public CustomerDeletedDomainEvent(Customer customer) => Customer = customer;
    }
}
