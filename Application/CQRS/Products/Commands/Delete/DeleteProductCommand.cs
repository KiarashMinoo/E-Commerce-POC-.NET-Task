using BuildingBlocks.Application.CQRS.Commands;

namespace Application.CQRS.Products.Commands.Delete
{
    public class DeleteProductCommand : ICommand<DeleteProductCommandResultDto>
    {
        public Guid Id { get; }

        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
