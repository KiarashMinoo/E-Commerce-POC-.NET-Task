using Application.Helpers;
using BuildingBlocks.Application.CQRS.Commands;
using Domain.Products;

namespace Application.CQRS.Products.Commands.Create
{
    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductCommandResultDto>
    {
        private readonly IPostgreSqlProductRepository repository;

        public CreateProductCommandHandler(IPostgreSqlProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateProductCommandResultDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var documents = await request.Image.StoreDocumentAsync("png", true, cancellationToken);
            var customer = new Product(request.Name, request.Quantity, request.Price, documents.Path, documents.ThumbnailPath!, repository);
            customer = await repository.AddAsync(customer, cancellationToken);
            customer = customer.ProductAddedEvent();

            var rtn = new CreateProductCommandResultDto() { Id = customer.Id };
            rtn.SetEntity(customer);
            return rtn;
        }
    }
}
