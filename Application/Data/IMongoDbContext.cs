using Domain.Customers;
using Domain.Products;
using MongoDB.Driver;

namespace Application.Data
{
    public interface IMongoDbContext
    {
        IMongoCollection<Customer> Customers { get; }
        IMongoCollection<Product> Products { get; }
    }
}
