using BuildingBlocks.Domain;
using Domain.Customers.BusinessRules.CustomerCellMustBeUnique;
using Domain.Customers.BusinessRules.CustomerEMailMustBeUnique;

namespace Domain.Customers
{
    public interface IPostgreSqlCustomerRepository : IRepository<Customer>, ICustomerCellMustBeUniqueHandler, ICustomerEMailMustBeUniqueHandler
    {
        Task<Customer> AddAsync(Customer customer, CancellationToken cancellationToken = default);
        Task<Customer?> RetrieveAsync(Guid id, CancellationToken cancellationToken = default);
        Task DeleteAsync(Customer customer, CancellationToken cancellationToken = default);
    }
}
