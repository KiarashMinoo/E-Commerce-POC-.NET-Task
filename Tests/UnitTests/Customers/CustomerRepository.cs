using Domain.Customers;
using Moq;

namespace UnitTests.Customers
{
    internal class CustomerRepository
    {
        private static readonly ICollection<Customer> _customers = new HashSet<Customer>();
        private readonly Mock<IPostgreSqlCustomerRepository> postgreMock = new();

        public IPostgreSqlCustomerRepository PostgreSqlRepository => postgreMock.Object;

        public CustomerRepository()
        {
            postgreMock.
                Setup(a => a.AddAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>())).
                ReturnsAsync((Customer customer, CancellationToken cancellationToken) =>
                {
                    _customers.Add(customer);
                    return customer;
                });

            postgreMock.
                Setup(a => a.RetrieveAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).
                ReturnsAsync((Guid id, CancellationToken cancellationToken) =>
                {
                    return _customers.FirstOrDefault(a => a.Id == id);
                });

            postgreMock.
                Setup(a => a.RetrieveByEMailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).
                ReturnsAsync((string eMail, CancellationToken cancellationToken) =>
                {
                    return _customers.FirstOrDefault(a => a.EMail == eMail);
                });
        }
    }
}
