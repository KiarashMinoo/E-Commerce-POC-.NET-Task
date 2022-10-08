using BuildingBlocks.Domain.DomainEvents;

namespace Domain.Customers.DomainEvents
{
    public class NewCustomerAddedDomainEvent : DomainEventBase
    {
        public string FullName { get; }
        public string EMail { get; }

        public NewCustomerAddedDomainEvent(string fullName, string eMail)
        {
            FullName = fullName;
            EMail = eMail;
        }
    }
}
