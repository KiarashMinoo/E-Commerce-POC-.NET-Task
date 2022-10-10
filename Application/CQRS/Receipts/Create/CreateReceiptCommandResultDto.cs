using BuildingBlocks.Application.CQRS.Commands;

namespace Application.CQRS.Receipts.Create
{
    public class CreateReceiptCommandResultDto : EntityResult
    {
        public Guid Id { get; set; }
    }
}
