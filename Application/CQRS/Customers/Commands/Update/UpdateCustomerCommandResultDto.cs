using BuildingBlocks.Application.CQRS.Commands;

namespace Application.CQRS.Customers.Commands.Update
{
    public class UpdateCustomerCommandResultDto : EntityResult
    {
        public Guid Id { get; set; }
    }
}
