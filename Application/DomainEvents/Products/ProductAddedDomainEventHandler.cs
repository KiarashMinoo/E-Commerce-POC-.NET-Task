using BuildingBlocks.Domain.DomainEvents;
using Domain.Products;
using Domain.Products.DomainEvents;

namespace Application.DomainEvents.Products
{
    internal class ProductAddedDomainEventHandler : IDomainEventHandler<ProductAddedDomainEvent>
    {
        private readonly IMongoDbProductRepository repository;

        public ProductAddedDomainEventHandler(IMongoDbProductRepository repository) => this.repository = repository;

        public Task Handle(ProductAddedDomainEvent notification, CancellationToken cancellationToken) => repository.AddAsync(notification.Product, cancellationToken);
    }
}
