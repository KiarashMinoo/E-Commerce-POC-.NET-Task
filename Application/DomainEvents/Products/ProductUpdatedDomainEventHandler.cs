using BuildingBlocks.Domain.DomainEvents;
using Domain.Products;
using Domain.Products.DomainEvents;

namespace Application.DomainEvents.Products
{
    internal class ProductUpdatedDomainEventHandler : IDomainEventHandler<ProductUpdatedDomainEvent>
    {
        private readonly IMongoDbProductRepository repository;

        public ProductUpdatedDomainEventHandler(IMongoDbProductRepository repository) => this.repository = repository;

        public Task Handle(ProductUpdatedDomainEvent notification, CancellationToken cancellationToken) => repository.UpdateAsync(notification.Product, cancellationToken);
    }
}
