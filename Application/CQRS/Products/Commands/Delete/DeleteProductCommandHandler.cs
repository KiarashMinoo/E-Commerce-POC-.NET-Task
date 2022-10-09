using BuildingBlocks.Application.CQRS.Commands;
using BuildingBlocks.Domain;
using Domain.Products;

namespace Application.CQRS.Products.Commands.Delete
{
    internal class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, DeleteProductCommandResultDto>
    {
        private readonly IPostgreSqlProductRepository repository;

        public DeleteProductCommandHandler(IPostgreSqlProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DeleteProductCommandResultDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var customer = await repository.RetrieveAsync(request.Id, cancellationToken) ?? throw new EntityNotFoundException<Product>();
            customer = customer.ProductDeletedEvent();

            await repository.DeleteAsync(customer, cancellationToken);

            var rtn = new DeleteProductCommandResultDto();
            rtn.SetEntity(customer);
            return rtn;
        }
    }
}
