using BuildingBlocks.Domain;

namespace Domain.Customers
{
    public interface IMongoDbCustomerRepository : IRepository<Customer>
    {
        Task<Customer> AddAsync(Customer customer, CancellationToken cancellationToken = default);
        Task<Customer?> RetrieveAsync(Guid id, CancellationToken cancellationToken = default);
        Task DeleteAsync(Customer customer, CancellationToken cancellationToken = default);
        Task<Customer> UpdateAsync(Customer customer, CancellationToken cancellationToken = default);
        Task<IEnumerable<Customer>> ListAllAsync(CancellationToken cancellationToken = default);
    }
}
