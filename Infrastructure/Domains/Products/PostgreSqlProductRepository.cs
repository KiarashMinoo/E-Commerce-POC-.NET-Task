using Application.Data;
using Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Domains.Products
{
    internal class PostgreSqlProductRepository : IPostgreSqlProductRepository
    {
        private readonly IPostgreSqlContext context;

        public PostgreSqlProductRepository(IPostgreSqlContext context)
        {
            this.context = context;
        }

        public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default)
        {
            var entry = await context.Products.AddAsync(product, cancellationToken);
            return entry.Entity;
        }

        public Task DeleteAsync(Product product, CancellationToken cancellationToken = default)
            => Task.Factory.StartNew(() => context.Products.Remove(product), cancellationToken);

        public bool ProductNameExistsByName(Guid productId, string name)
            => context.Products.Any(a => a.Name == name && a.Id != productId);

        public Task<Product?> RetrieveAsync(Guid id, CancellationToken cancellationToken = default)
            => context.Products.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }
}
