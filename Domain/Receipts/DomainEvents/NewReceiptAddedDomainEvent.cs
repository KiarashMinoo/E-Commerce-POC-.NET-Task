using BuildingBlocks.Domain.DomainEvents;

namespace Domain.Receipts.DomainEvents
{
    public class NewReceiptAddedDomainEvent : DomainEventBase
    {
        public Guid ProductId { get; }

        public NewReceiptAddedDomainEvent(Guid productId) => ProductId = productId;
    }
}
