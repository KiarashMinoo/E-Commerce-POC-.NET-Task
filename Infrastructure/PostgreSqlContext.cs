using Application.CQRS.Customers.Commands.Create;
using Application.Data;
using DnsClient.Internal;
using Domain.Customers;
using Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    internal class PostgreSqlContext : DbContext, IPostgreSqlContext
    {
        private static bool isMigrating = false;
        private static bool migrated = false;
        private readonly ILogger<PostgreSqlContext> logger;
        private readonly IMediator mediator;

        public DbSet<Customer> Customers { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;

        public PostgreSqlContext(ILogger<PostgreSqlContext> logger, DbContextOptions<PostgreSqlContext> options, IMediator mediator) : base(options)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        public async Task MigrateAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                if (!migrated && !isMigrating)
                {
                    if (isMigrating)
                    {
                        while (!migrated)
                            await Task.Delay(10, cancellationToken);

                        return;
                    }

                    isMigrating = true;

                    await Database.MigrateAsync(cancellationToken: cancellationToken);
                    await SeedAsync(cancellationToken);

                    isMigrating = false;
                    migrated = true;
                }
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Error has occurred while database migration");
            }
        }

        private async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            await mediator.Send(new CreateCustomerCommand("Ibrahim Elmeligy", "ielmeligy@creativeadvtech.com", "+971565065300"), cancellationToken);
            await mediator.Send(new CreateCustomerCommand("Ahmad(Kia) Minoo", "ahmadminoo@gmail.com", "+989123394363"), cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgreSqlContext).Assembly);
        }
    }
}
