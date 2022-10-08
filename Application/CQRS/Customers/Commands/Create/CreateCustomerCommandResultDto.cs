using BuildingBlocks.Application.CQRS.Commands;

namespace Application.CQRS.Customers.Commands.Create
{
    public class CreateCustomerCommandResultDto : EntityResult
    {
        public Guid Id { get; set; }
    }
}
