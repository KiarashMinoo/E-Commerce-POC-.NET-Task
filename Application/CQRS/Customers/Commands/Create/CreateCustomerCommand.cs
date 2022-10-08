using BuildingBlocks.Application.CQRS.Commands;

namespace Application.CQRS.Customers.Commands.Create
{
    public class CreateCustomerCommand : ICommand<CreateCustomerCommandResultDto>
    {
        public string FullName { get; }
        public string EMail { get; }
        public string Cell { get; }

        public CreateCustomerCommand(string fullName, string eMail, string cell)
        {
            FullName = fullName;
            EMail = eMail;
            Cell = cell;
        }
    }
}
