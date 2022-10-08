namespace Domain.Customers
{
    public interface IMongoDbCustomerRepository : IPostgreSqlCustomerRepository
    {
        Task<Customer> UpdateAsync(Customer customer, CancellationToken cancellationToken = default);
        Task<IEnumerable<Customer>> ListAllAsync(CancellationToken cancellationToken = default);
    }
}
