using BuildingBlocks.Domain;
using Domain.Customers.BusinessRules.CustomerCellMustBeUnique;
using Domain.Customers.BusinessRules.CustomerEMailMustBeUnique;
using Domain.Customers.DomainEvents;

namespace Domain.Customers
{
    public class Customer : EntityBase, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; } = null!;
        public string EMail { get; private set; } = null!;
        public string Cell { get; private set; } = null!;

        private Customer() => Id = Guid.NewGuid();

        public Customer(string fullName, string eMail, string cell, ICustomerCellMustBeUniqueHandler cellHandler, ICustomerEMailMustBeUniqueHandler eMailHandler) : this()
        {
            CheckBusinessRule(new CustomerCellMustBeUniqueBusinessRule(cellHandler, Id, cell));
            CheckBusinessRule(new CustomerEMailMustBeUniqueBusinessRule(eMailHandler, Id, eMail));

            FullName = fullName;
            EMail = eMail;
            Cell = cell;
        }

        public Customer Update(string fullName, string eMail, string cell, ICustomerCellMustBeUniqueHandler cellHandler, ICustomerEMailMustBeUniqueHandler eMailHandler)
        {
            CheckBusinessRule(new CustomerCellMustBeUniqueBusinessRule(cellHandler, Id, cell));
            CheckBusinessRule(new CustomerEMailMustBeUniqueBusinessRule(eMailHandler, Id, eMail));

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
