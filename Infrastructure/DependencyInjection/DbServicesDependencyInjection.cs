using Application.Data;
using Domain.Customers;
using Infrastructure.Configurations;
using Infrastructure.Domains.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure.DependencyInjection
{
    public static class DbServicesDependencyInjection
    {
        public static IServiceCollection AddDbService(IServiceCollection services, IConfiguration configuration, string postgreSqlConnectionStringName, string mongoDbSectionName)
        {
            services.AddDbContext<IPostgreSqlContext, PostgreSqlContext>((provider, options) =>
            {
                options.UseNpgsql(configuration.GetConnectionString(postgreSqlConnectionStringName));
            });

            services.Configure<MongoDbConfiguration>(configuration.GetSection(mongoDbSectionName));

            services.TryAddScoped<IMongoDbContext, MongoDbContext>();

            services.TryAddScoped<IPostgreSqlCustomerRepository, PostgreSqlCustomerRepository>();
            services.TryAddScoped<IMongoDbCustomerRepository, MongoDbCustomerRepository>();

            CustomerMongoDbClassMap.Register();


            return services;
        }
    }
}
