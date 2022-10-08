using MediatR;

namespace BuildingBlocks.Domain.DomainEvents
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}
