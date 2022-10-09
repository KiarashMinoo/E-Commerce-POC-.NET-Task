using Application.CQRS.Customers.Commands.Create;
using Application.Data;
using Domain.Customers;
using Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    internal class PostgreSqlContext : DbContext, IPostgreSqlContext
    {
        private static bool isMigrating = false;
        private static bool migrated = false;

        private readonly IMediator mediator;

        public DbSet<Customer> Customers { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;

        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options, IMediator mediator) : base(options)
        {
            MigrateAsync().Wait();
            this.mediator = mediator;
        }

        private async Task MigrateAsync()
        {
            var pendingMigrations = Database.GetPendingMigrations();
            if (pendingMigrations.Any() && !migrated)
            {
                if (isMigrating)
                {
                    while (!migrated)
                        await Task.Delay(10);

                    return;
                }

                isMigrating = true;

                await Database.MigrateAsync();
                await SeedAsync();

                isMigrating = false;
                migrated = true;
            }
        }

        private async Task SeedAsync()
        {
            await mediator.Send(new CreateCustomerCommand("Ibrahim Elmeligy", "ielmeligy@creativeadvtech.com", "+971 56 506 5300"));
            await mediator.Send(new CreateCustomerCommand("Ahmad(Kia) Minoo", "ahmadminoo@gmail.com", "+989 12 339 4363"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgreSqlContext).Assembly);
        }
    }
}
