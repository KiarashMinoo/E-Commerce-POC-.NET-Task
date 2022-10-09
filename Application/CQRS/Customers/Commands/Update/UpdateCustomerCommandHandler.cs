using BuildingBlocks.Application.CQRS.Commands;
using BuildingBlocks.Domain;
using Domain.Customers;

namespace Application.CQRS.Customers.Commands.Update
{
    internal class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand, UpdateCustomerCommandResultDto>
    {
        private readonly IPostgreSqlCustomerRepository repository;

        public UpdateCustomerCommandHandler(IPostgreSqlCustomerRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UpdateCustomerCommandResultDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await repository.RetrieveAsync(request.Id, cancellationToken) ?? throw new EntityNotFoundException<Customer>();
            customer = customer.Update(request.FullName, request.EMail, request.Cell, repository, repository);
            customer = customer.CustomerUpdatedEvent();

            var rtn = new UpdateCustomerCommandResultDto() { Id = customer.Id };
            rtn.SetEntity(customer);
            return rtn;
        }
    }
}
