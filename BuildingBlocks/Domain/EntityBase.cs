using BuildingBlocks.Domain.BusinessRules;
using BuildingBlocks.Domain.DomainEvents;

namespace BuildingBlocks.Domain
{
    public class EntityBase
    {
        private HashSet<IDomainEvent>? _domainEvents;

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents != null ? _domainEvents : Enumerable.Empty<IDomainEvent>().ToList().AsReadOnly();

        public void ClearDomainEvents() => _domainEvents?.Clear();

        protected void AddDomainEvent(IDomainEvent domainEvent) => (_domainEvents ??= new HashSet<IDomainEvent>()).Add(domainEvent);

        protected void CheckBusinessRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
                throw new BusinessRuleValidationException(rule);
        }
    }
}
