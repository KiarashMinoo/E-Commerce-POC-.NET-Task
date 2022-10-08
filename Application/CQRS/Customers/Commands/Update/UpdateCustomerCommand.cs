using BuildingBlocks.Application.CQRS.Commands;

namespace Application.CQRS.Customers.Commands.Update
{
    public class UpdateCustomerCommand : ICommand<UpdateCustomerCommandResultDto>
    {
        public Guid Id { get; }
        public string FullName { get; }
        public string EMail { get; }
        public string Cell { get; }

        public UpdateCustomerCommand(Guid id, string fullName, string eMail, string cell)
        {
            Id = id;
            FullName = fullName;
            EMail = eMail;
            Cell = cell;
        }
    }
}
