using BuildingBlocks.Application.CQRS.Commands;

namespace Application.CQRS.Products.Commands.Update
{
    public class UpdateProductCommandResultDto : EntityResult
    {
        public Guid Id { get; set; }
    }
}
