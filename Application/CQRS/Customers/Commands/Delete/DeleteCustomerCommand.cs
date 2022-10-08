using BuildingBlocks.Application.CQRS.Commands;

namespace Application.CQRS.Customers.Commands.Delete
{
    public class DeleteCustomerCommand : ICommand<DeleteCustomerCommandResultDto>
    {
        public Guid Id { get; }

        public DeleteCustomerCommand(Guid id)
        {
            Id = id;
        }
    }
}
