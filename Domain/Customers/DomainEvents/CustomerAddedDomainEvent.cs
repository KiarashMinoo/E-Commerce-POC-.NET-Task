using BuildingBlocks.Domain.DomainEvents;

namespace Domain.Customers.DomainEvents
{
    public class CustomerAddedDomainEvent : DomainEventBase
    {
        public Customer Customer { get; }

        public CustomerAddedDomainEvent(Customer customer) => Customer = customer;
    }
}
