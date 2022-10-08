using BuildingBlocks.Application.CQRS.Commands;
using Domain.Customers;

namespace Application.CQRS.Customers.Commands.Create
{
    internal class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, CreateCustomerCommandResultDto>
    {
        private readonly IPostgreSqlCustomerRepository repository;

        public CreateCustomerCommandHandler(IPostgreSqlCustomerRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateCustomerCommandResultDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer(request.FullName, request.EMail, request.Cell);
            customer = await repository.AddAsync(customer, cancellationToken);
            customer = customer.CustomerAddedEvent();

            var rtn = new CreateCustomerCommandResultDto() { Id = customer.Id };
            rtn.SetEntity(customer);
            return rtn;
        }
    }
}
