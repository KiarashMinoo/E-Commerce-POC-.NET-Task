using Application.Helpers;
using BuildingBlocks.Application.CQRS.Commands;
using BuildingBlocks.Domain;
using Domain.Products;

namespace Application.CQRS.Products.Commands.Update
{
    internal class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, UpdateProductCommandResultDto>
    {
        private readonly IPostgreSqlProductRepository repository;

        public UpdateProductCommandHandler(IPostgreSqlProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UpdateProductCommandResultDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var customer = await repository.RetrieveAsync(request.Id, cancellationToken) ?? throw new EntityNotFoundException<Product>();
            if (request.Image is not null)
            {
                string fileName = Guid.NewGuid().ToString();
                if (!string.IsNullOrEmpty(customer.Image))
                    fileName = customer.Image.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries).Last().Split(new[] { '.' }).First();

                var documents = await request.Image.StoreDocumentAsync(fileName, "png", true, cancellationToken);
                customer = customer.Update(request.Name, request.Quantity, request.Price, documents.Path, documents.ThumbnailPath!, repository);
            }
            else
                customer = customer.Update(request.Name, request.Quantity, request.Price, customer.Image, customer.Thumbnail, repository);

            customer = customer.ProductUpdatedEvent();

            var rtn = new UpdateProductCommandResultDto() { Id = customer.Id };
            rtn.SetEntity(customer);
            return rtn;
        }
    }
}
