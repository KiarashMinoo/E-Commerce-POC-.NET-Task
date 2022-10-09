using Application.Helpers;
using BuildingBlocks.Application.CQRS.Commands;
using Domain.Customers;
using Domain.Users;
using Domain.Users.SharedKernel;

namespace Application.CQRS.Users.Commands.Register
{
    internal class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, RegisterUserCommandResultDto>
    {
        private readonly IUserRepository repository;
        private readonly IPostgreSqlCustomerRepository customerRepository;

        public RegisterUserCommandHandler(IUserRepository repository, IPostgreSqlCustomerRepository customerRepository)
        {
            this.repository = repository;
            this.customerRepository = customerRepository;
        }

        public async Task<RegisterUserCommandResultDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.RetrieveByEMailAsync(request.EMail, cancellationToken);
            if (customer is null)
            {
                customer = new Customer(request.FullName, request.EMail, request.Cell, customerRepository, customerRepository);
                customer = await customerRepository.AddAsync(customer, cancellationToken);
                customer = customer.CustomerAddedEvent();
            }

            var salt = KeyProvider.CreateNewSalt();
            var user = new User(request.UserName, request.Password.Encrypt(KeyProvider.Key, salt), salt, customer.Id, repository);
            _ = await repository.AddAsync(user, cancellationToken);

            var rtn = new RegisterUserCommandResultDto() { Id = user.Id };
            rtn.SetEntity(customer);
            return rtn;
        }
    }
}
