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
            customer = customer.Update(request.Name, request.Quantity, request.Price, repository);
            customer = customer.ProductUpdatedEvent();

            var rtn = new UpdateProductCommandResultDto() { Id = customer.Id };
            rtn.SetEntity(customer);
            return rtn;
        }
    }
}
