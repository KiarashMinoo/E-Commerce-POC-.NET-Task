using Application.Data;
using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Domains.Customers
{
    internal class PostgreSqlCustomerRepository : IPostgreSqlCustomerRepository
    {
        private readonly IPostgreSqlContext context;

        public PostgreSqlCustomerRepository(IPostgreSqlContext context)
        {
            this.context = context;
        }

        public async Task<Customer> AddAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            var entry = await context.Customers.AddAsync(customer, cancellationToken);
            return entry.Entity;
        }

        public Task DeleteAsync(Customer customer, CancellationToken cancellationToken = default)
            => Task.Factory.StartNew(() => context.Customers.Remove(customer), cancellationToken);

        public Task<Customer?> RetrieveAsync(Guid id, CancellationToken cancellationToken = default)
            => context.Customers.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }
}
