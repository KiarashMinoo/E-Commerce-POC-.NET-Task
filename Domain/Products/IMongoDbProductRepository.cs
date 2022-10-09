using BuildingBlocks.Domain;

namespace Domain.Products
{
    public interface IMongoDbProductRepository : IRepository<Product>
    {
        Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default);
        Task<Product?> RetrieveAsync(Guid id, CancellationToken cancellationToken = default);
        Task DeleteAsync(Product product, CancellationToken cancellationToken = default);
        Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> ListAllAsync(CancellationToken cancellationToken = default);
    }
}
