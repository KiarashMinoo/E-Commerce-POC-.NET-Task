using BuildingBlocks.Infrastructure.Mediatr;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Infrastructure.DependencyInjection
{
    public static class CommandHandlerEntityEventsExtension
    {
        public static IServiceCollection AddCommandHandlerEntityEvent(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            services.Add(new ServiceDescriptor(typeof(IRequestPostProcessor<,>), typeof(CommandHandlerEntityEventsPostProcessor<,>), lifetime));
            return services;
        }
    }
}
