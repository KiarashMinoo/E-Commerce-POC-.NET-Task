using BuildingBlocks.Domain.DomainEvents;

namespace Domain.Products.DomainEvents
{
    public class NewProductAddedDomainEvent : DomainEventBase
    {
        public string Name { get; }

        public int Quantity { get; }

        public NewProductAddedDomainEvent(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }
    }
}
