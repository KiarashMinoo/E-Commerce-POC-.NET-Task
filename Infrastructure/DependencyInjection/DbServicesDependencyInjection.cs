using Application.Data;
using Domain.Customers;
using Domain.Products;
using Infrastructure.Configurations;
using Infrastructure.Domains.Customers;
using Infrastructure.Domains.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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

            return services;
        }
    }
}
