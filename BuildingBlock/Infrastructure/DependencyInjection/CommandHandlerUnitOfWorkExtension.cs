using BuildingBlocks.Infrastructure.Mediatr;
using MediatR;

namespace Microsoft.Extensions.DependencyInjection
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
