using Application.Data;
using Domain.Customers;
using Domain.Products;
using Domain.Receipts;
using Domain.Users;
using Infrastructure.Configurations;
using Infrastructure.Domains.Customers;
using Infrastructure.Domains.Products;
using Infrastructure.Domains.Receipts;
using Infrastructure.Domains.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics;

namespace Infrastructure.DependencyInjection
{
    public static class DbServicesDependencyInjection
    {
        public static IServiceCollection AddDbServices(this IServiceCollection services, IConfiguration configuration, string postgreSqlConnectionStringName, string mongoDbSectionName)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            //PostgreSql Config
            services.AddDbContext<IPostgreSqlContext, PostgreSqlContext>((provider, options) =>
            {
                options.UseNpgsql(configuration.GetConnectionString(postgreSqlConnectionStringName));
            });

            if (!Debugger.IsAttached)
            {
                var provider = services.BuildServiceProvider();
                provider.GetRequiredService<IPostgreSqlContext>().MigrateAsync();
            }

            //MongoDb Config
            services.Configure<MongoDbConfiguration>(configuration.GetSection(mongoDbSectionName));
            services.TryAddScoped<IMongoDbContext, MongoDbContext>();
            CustomerMongoDbClassMap.Register();
            ProductMongoDbClassMap.Register();

            //Repositories
            services.TryAddScoped<IPostgreSqlCustomerRepository, PostgreSqlCustomerRepository>();
            services.TryAddScoped<IMongoDbCustomerRepository, MongoDbCustomerRepository>();

            services.TryAddScoped<IPostgreSqlProductRepository, PostgreSqlProductRepository>();
            services.TryAddScoped<IMongoDbProductRepository, MongoDbProductRepository>();

            services.TryAddScoped<IUserRepository, UserRepository>();
            services.TryAddScoped<IReceiptRepository, ReceiptRepository>();

            return services;
        }
    }
}
