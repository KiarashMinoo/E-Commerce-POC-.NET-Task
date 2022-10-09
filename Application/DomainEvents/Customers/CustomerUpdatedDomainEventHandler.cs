using BuildingBlocks.Domain.DomainEvents;
using Domain.Customers;
using Domain.Customers.DomainEvents;

namespace Application.DomainEvents.Customers
{
    internal class CustomerUpdatedDomainEventHandler : IDomainEventHandler<CustomerUpdatedDomainEvent>
    {
        private readonly IMongoDbCustomerRepository repository;

        public CustomerUpdatedDomainEventHandler(IMongoDbCustomerRepository repository) => this.repository = repository;

        public Task Handle(CustomerUpdatedDomainEvent notification, CancellationToken cancellationToken) => repository.UpdateAsync(notification.Customer, cancellationToken);
    }
}
