using BuildingBlocks.Domain;
using Domain.Customers.DomainEvents;

namespace Domain.Customers
{
    public class Customer : EntityBase, IAggregateRoot
    {
        public Guid Id { get; }
        public string FullName { get; private set; } = null!;
        public string EMail { get; private set; } = null!;
        public string Cell { get; private set; } = null!;

        private Customer() => Id = Guid.NewGuid();

        public Customer(Guid id, string fullName, string eMail, string cell) : this()
        {
            Id = id;
            FullName = fullName;
            EMail = eMail;
            Cell = cell;
        }

        public Customer(string fullName, string eMail, string cell) : this(Guid.NewGuid(), fullName, eMail, cell)
        {
        }

        public Customer Update(string fullName, string eMail, string cell)
        {
            FullName = fullName;
            EMail = eMail;
            Cell = cell;

            return this;
        }

        public Customer CustomerAddedEvent()
        {
            AddDomainEvent(new CustomerAddedDomainEvent(this));

            return this;
        }

        public Customer CustomerUpdatedEvent()
        {
            AddDomainEvent(new CustomerUpdatedDomainEvent(this));

            return this;
        }

        public Customer CustomerDeletedEvent()
        {
            AddDomainEvent(new CustomerDeletedDomainEvent(this));

            return this;
        }
    }
}
