using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Application.Data
{
    public interface IPostgreSqlContext
    {
        DbSet<Customer> Customers { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
