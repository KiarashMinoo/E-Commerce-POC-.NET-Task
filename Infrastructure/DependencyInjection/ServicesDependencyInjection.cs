using Application.CQRS.Customers.Commands.Create;
using Application.CQRS.Customers.Queries.ListAll;
using BuildingBlocks.Application.Data;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Infrastructure.DependencyInjection
{
    public static class ServicesDependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateCustomerCommand).Assembly);
            services.AddCommandHandlerFluentValidation(new[] { typeof(CreateCustomerCommandValidator).Assembly });
            services.AddCommandHandlerUnitOfWork();

            var automapperAssemblies = new List<Assembly>() { typeof(ListAllCustomerQuery).Assembly };
            services.AddAutoMapper(automapperAssemblies.ToArray());

            services.TryAddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
