namespace BuildingBlocks.Domain.DomainEvents
{
    public abstract class DomainEventBase : IDomainEvent
    {
        public DateTime OccurredOn { get; }        

        public DomainEventBase() 
        {
            OccurredOn = DateTime.Now;
        }
    }
}
