using BuildingBlocks.Infrastructure.Mediatr;
using FluentValidation;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Infrastructure.DependencyInjection
{
    public static class CommandHandlerFluentValidationExtension
    {
        public static IServiceCollection AddCommandHandlerFluentValidation(this IServiceCollection services, IEnumerable<Assembly> assemblies,
            ServiceLifetime lifetime = ServiceLifetime.Transient, Func<AssemblyScanner.AssemblyScanResult, bool>? filter = null)
        {
            services.Add(new ServiceDescriptor(typeof(IRequestPreProcessor<>), typeof(CommandHandlerFluentValidationPreProcessor<>), lifetime));

            services.AddValidatorsFromAssemblies(assemblies, lifetime, filter);

            return services;
        }
    }
}
