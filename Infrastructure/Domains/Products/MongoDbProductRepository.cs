using Application.Data;
using Domain.Products;
using MongoDB.Driver;

namespace Infrastructure.Domains.Products
{
    internal class MongoDbProductRepository : IMongoDbProductRepository
    {
        private readonly IMongoDbContext context;

        public MongoDbProductRepository(IMongoDbContext context)
        {
            this.context = context;
        }

        public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default)
        {
            await context.Products.InsertOneAsync(product, new InsertOneOptions() { }, cancellationToken);
            return product;
        }

        public Task DeleteAsync(Product product, CancellationToken cancellationToken = default)
        {
            var filter = Builders<Product>.Filter.Eq(a => a.Id, product.Id);
            return context.Products.DeleteOneAsync(filter, cancellationToken);
        }

        public async Task<IEnumerable<Product>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            var filter = Builders<Product>.Filter.Empty;
            return await context.Products.Find(filter).ToListAsync(cancellationToken);
        }

        public async Task<Product?> RetrieveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<Product>.Filter.Eq(a => a.Id, id);
            return await context.Products.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            var filter = Builders<Product>.Filter.Eq(a => a.Id, product.Id);
            await context.Products.ReplaceOneAsync(filter, product, new ReplaceOptions() { }, cancellationToken);
            return product;
        }
    }
}
