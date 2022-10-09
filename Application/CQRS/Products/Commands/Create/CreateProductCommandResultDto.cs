using BuildingBlocks.Application.CQRS.Commands;

namespace Application.CQRS.Products.Commands.Create
{
    public class CreateProductCommandResultDto : EntityResult
    {
        public Guid Id { get; set; }
    }
}
