namespace BuildingBlocks.Domain.DomainEvents
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
}
