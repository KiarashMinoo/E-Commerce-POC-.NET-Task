using BuildingBlocks.Infrastructure.Mediatr;
using FluentValidation;
using MediatR.Pipeline;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
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
