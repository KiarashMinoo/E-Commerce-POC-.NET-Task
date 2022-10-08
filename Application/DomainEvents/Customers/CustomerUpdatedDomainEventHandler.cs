using BuildingBlocks.Domain;
using BuildingBlocks.Domain.DomainEvents;
using Domain.Customers;
using Domain.Customers.DomainEvents;

namespace Application.DomainEvents.Customers
{
    internal class CustomerUpdatedDomainEventHandler : IDomainEventHandler<CustomerUpdatedDomainEvent>
    {
        private readonly IMongoDbCustomerRepository repository;

        public CustomerUpdatedDomainEventHandler(IMongoDbCustomerRepository repository) => this.repository = repository;

        public async Task Handle(CustomerUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var customer = await repository.RetrieveAsync(notification.Customer.Id, cancellationToken) ?? throw new EntityNotFoundException<Customer>();
            customer = customer.Update(notification.Customer.FullName, notification.Customer.EMail, notification.Customer.Cell);
            await repository.UpdateAsync(customer, cancellationToken);
        }
    }
}
