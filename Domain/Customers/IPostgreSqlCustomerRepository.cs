using BuildingBlocks.Domain;

namespace Domain.Customers
{
    public interface IPostgreSqlCustomerRepository : IRepository<Customer>
    {
        Task<Customer> AddAsync(Customer customer, CancellationToken cancellationToken = default);
        Task<Customer?> RetrieveAsync(Guid id, CancellationToken cancellationToken = default);
        Task DeleteAsync(Customer customer, CancellationToken cancellationToken = default);
    }
}
