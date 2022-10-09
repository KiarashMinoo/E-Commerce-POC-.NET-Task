using BuildingBlocks.Infrastructure.Mediatr;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Infrastructure.DependencyInjection
{
    public static class CommandHandlerUnitOfWorkExtension
    {
        public static IServiceCollection AddCommandHandlerUnitOfWork(this IServiceCollection services)
        {
            services.Decorate(typeof(IRequestHandler<,>), typeof(CommandHandlerUnitOfWorkDecorator<,>));
            return services;
        }
    }
}
