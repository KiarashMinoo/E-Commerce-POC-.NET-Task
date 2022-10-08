using BuildingBlocks.Domain.DomainEvents;
using Domain.Customers;
using Domain.Customers.DomainEvents;

namespace Application.DomainEvents.Customers
{
    internal class CustomerAddedDomainEventHandler : IDomainEventHandler<CustomerAddedDomainEvent>
    {
        private readonly IMongoDbCustomerRepository repository;

        public CustomerAddedDomainEventHandler(IMongoDbCustomerRepository repository) => this.repository = repository;

        public Task Handle(CustomerAddedDomainEvent notification, CancellationToken cancellationToken) => repository.AddAsync(notification.Customer, cancellationToken);
    }
}
