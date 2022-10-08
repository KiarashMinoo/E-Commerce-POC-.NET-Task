using BuildingBlocks.Application.CQRS.Commands;
using BuildingBlocks.Domain;
using Domain.Customers;

namespace Application.CQRS.Customers.Commands.Delete
{
    internal class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand, DeleteCustomerCommandResultDto>
    {
        private readonly IPostgreSqlCustomerRepository repository;

        public DeleteCustomerCommandHandler(IPostgreSqlCustomerRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DeleteCustomerCommandResultDto> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await repository.RetrieveAsync(request.Id, cancellationToken) ?? throw new EntityNotFoundException<Customer>();
            customer = customer.CustomerDeletedEvent();

            await repository.DeleteAsync(customer, cancellationToken);

            var rtn = new DeleteCustomerCommandResultDto();
            rtn.SetEntity(customer);
            return rtn;
        }
    }
}
