using BuildingBlocks.Domain;
using Domain.Products.BusinessRules.ProductNameMustBeUnique;

namespace Domain.Products
{
    public interface IPostgreSqlProductRepository : IRepository<Product>, IProductNameMustBeUniqueHandler
    {
        Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default);
        Task<Product?> RetrieveAsync(Guid id, CancellationToken cancellationToken = default);
        Task DeleteAsync(Product product, CancellationToken cancellationToken = default);
    }
}
