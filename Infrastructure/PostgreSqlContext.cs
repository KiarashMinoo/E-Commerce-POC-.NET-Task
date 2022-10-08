using Application.Data;
using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class PostgreSqlContext : DbContext, IPostgreSqlContext
    {
        private static readonly object mutex = new();

        public DbSet<Customer> Customers { get; set; } = null!;

        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options)
        {
            Migrate();
        }

        private void Migrate()
        {
            var pendingMigrations = Database.GetPendingMigrations();
            if (pendingMigrations.Any())
            {
                lock (mutex)
                {
                    Database.Migrate();

                    Seed();
                }
            }
        }

        private void Seed()
        {
            if (!Customers.Any(a => a.EMail == "ielmeligy@creativeadvtech.com"))
                Customers.Add(new Customer("Ibrahim Elmeligy", "ielmeligy@creativeadvtech.com", "+971 56 506 5300"));

            if (!Customers.Any(a => a.EMail == "ahmadminoo@gmail.com"))
                Customers.Add(new Customer("Ahmad(Kia) Minoo", "ahmadminoo@gmail.com", "+989 12 339 4363"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgreSqlContext).Assembly);
        }
    }
}
