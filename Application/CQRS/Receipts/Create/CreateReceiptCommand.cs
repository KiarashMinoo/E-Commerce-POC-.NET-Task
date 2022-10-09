using BuildingBlocks.Application.CQRS.Commands;

namespace Application.CQRS.Receipts.Create
{
    public class CreateReceiptCommand : ICommand<CreateReceiptCommandResultDto>
    {
        public Guid ProductId { get; }
        public int Quantity { get; }

        public CreateReceiptCommand(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
