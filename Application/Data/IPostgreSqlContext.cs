using Domain.Customers;
using Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Application.Data
{
    public interface IPostgreSqlContext
    {
        DbSet<Customer> Customers { get; }
        DbSet<Product> Products { get; }

        Task MigrateAsync(CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
