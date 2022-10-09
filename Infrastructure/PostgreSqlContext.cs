using Application.CQRS.Customers.Commands.Create;
using Application.Data;
using DnsClient.Internal;
using Domain.Customers;
using Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

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

            MigrateAsync().Wait();
        }

        public async Task MigrateAsync()
        {
            if (Debugger.IsAttached)
                return;

            try
            {
                if (!migrated && !isMigrating)
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
            catch (Exception exception)
            {
                logger.LogError(exception, "Error has occurred while database migration");
            }
        }

        private async Task SeedAsync()
        {
            await mediator.Send(new CreateCustomerCommand("Ibrahim Elmeligy", "ielmeligy@creativeadvtech.com", "+971565065300"));
            await mediator.Send(new CreateCustomerCommand("Ahmad(Kia) Minoo", "ahmadminoo@gmail.com", "+989123394363"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgreSqlContext).Assembly);
        }
    }
}
