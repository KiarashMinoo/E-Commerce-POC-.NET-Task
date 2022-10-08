namespace Domain.Customers
{
    public interface IMongoDbCustomerRepository : IPostgreSqlCustomerRepository
    {
        Task<IEnumerable<Customer>> ListAllAsync(CancellationToken cancellationToken = default);
    }
}
