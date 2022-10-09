using Domain.Customers;
using Domain.Products;
using Domain.Receipts;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Data
{
    public interface IPostgreSqlContext
    {
        DbSet<Customer> Customers { get; }
        DbSet<Product> Products { get; }
        DbSet<User> Users { get; }
        DbSet<Receipt> Receipts { get; }

        Task MigrateAsync(CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
