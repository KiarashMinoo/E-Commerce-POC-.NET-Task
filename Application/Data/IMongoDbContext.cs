using Domain.Customers;
using MongoDB.Driver;

namespace Application.Data
{
    public interface IMongoDbContext
    {
        IMongoCollection<Customer> Customers { get; }
    }
}
