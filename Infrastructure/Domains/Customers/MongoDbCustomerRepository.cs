using Application.Data;
using Domain.Customers;
using MongoDB.Driver;

namespace Infrastructure.Domains.Customers
{
    internal class MongoDbCustomerRepository : IMongoDbCustomerRepository
    {
        private readonly IMongoDbContext context;

        public MongoDbCustomerRepository(IMongoDbContext context)
        {
            this.context = context;
        }

        public async Task<Customer> AddAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            await context.Customers.InsertOneAsync(customer, new InsertOneOptions() { }, cancellationToken);
            return customer;
        }

        public Task DeleteAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            var filter = Builders<Customer>.Filter.Eq(a => a.Id, customer.Id);
            return context.Customers.DeleteOneAsync(filter, cancellationToken);
        }

        public async Task<IEnumerable<Customer>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            var filter = Builders<Customer>.Filter.Empty;
            return await context.Customers.Find(filter).ToListAsync(cancellationToken);
        }

        public async Task<Customer?> RetrieveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<Customer>.Filter.Eq(a => a.Id, id);
            return await context.Customers.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
