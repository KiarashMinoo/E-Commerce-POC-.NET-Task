using Application.Data;
using Domain.Customers;
using Domain.Products;
using Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure
{
    internal class MongoDbContext : IMongoDbContext
    {
        public IMongoCollection<Customer> Customers { get; }

        public IMongoCollection<Product> Products { get; }

        public MongoDbContext(IOptionsSnapshot<MongoDbConfiguration> configuration)
        {
            var client = new MongoClient(configuration.Value.ConnectionString);
            var database = client.GetDatabase(configuration.Value.DatabaseName);

            Customers = database.GetCollection<Customer>(configuration.Value.CustomersCollectionName);
            Products = database.GetCollection<Product>(configuration.Value.ProductsCollectionName);
        }
    }
}
