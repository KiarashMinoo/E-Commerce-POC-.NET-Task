using BuildingBlocks.Domain.DomainEvents;
using Domain.Customers;
using Domain.Customers.DomainEvents;

namespace Application.DomainEvents.Customers
{
    internal class CustomerDeletedDomainEventHandler : IDomainEventHandler<CustomerDeletedDomainEvent>
    {
        private readonly IMongoDbCustomerRepository repository;

        public CustomerDeletedDomainEventHandler(IMongoDbCustomerRepository repository) => this.repository = repository;

        public Task Handle(CustomerDeletedDomainEvent notification, CancellationToken cancellationToken) => repository.DeleteAsync(notification.Customer, cancellationToken);
    }
}
