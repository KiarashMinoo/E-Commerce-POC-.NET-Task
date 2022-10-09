using BuildingBlocks.Domain.DomainEvents;
using Domain.Products;
using Domain.Products.DomainEvents;

namespace Application.DomainEvents.Products
{
    internal class ProductDeletedDomainEventHandler : IDomainEventHandler<ProductDeletedDomainEvent>
    {
        private readonly IMongoDbProductRepository repository;

        public ProductDeletedDomainEventHandler(IMongoDbProductRepository repository) => this.repository = repository;

        public Task Handle(ProductDeletedDomainEvent notification, CancellationToken cancellationToken) => repository.DeleteAsync(notification.Product, cancellationToken);
    }
}
