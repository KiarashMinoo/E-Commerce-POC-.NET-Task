using BuildingBlocks.Domain.DomainEvents;

namespace Domain.Customers.DomainEvents
{
    public class CustomerUpdatedDomainEvent : DomainEventBase
    {
        public Customer Customer { get; }

        public CustomerUpdatedDomainEvent(Customer customer) => Customer = customer;
    }
}
