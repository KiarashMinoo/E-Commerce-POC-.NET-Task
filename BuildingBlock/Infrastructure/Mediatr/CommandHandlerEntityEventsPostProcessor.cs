using BuildingBlocks.Application.CQRS.Commands;
using BuildingBlocks.Domain;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Infrastructure.Mediatr
{
    internal class CommandHandlerEntityEventsPostProcessor<TRequest, TResult> : IRequestPostProcessor<TRequest, TResult> where TRequest : IRequest<TResult>
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public CommandHandlerEntityEventsPostProcessor(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public Task Process(TRequest request, TResult result, CancellationToken _)
        {
            if (request is ICommand command && result is EntityResult entityResult)
            {
                var entity = entityResult.GetEntity<EntityBase>();
                if (entity != null)
                {
                    var domainEvents = entity.DomainEvents.ToList();
                    if (domainEvents.Any())
                    {
                        foreach (var domainEvent in domainEvents)
                        {
                            Task.Factory.StartNew(async () =>
                            {
                                using var scope = serviceScopeFactory.CreateScope();

                                try
                                {
                                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                                    await mediator.Publish(domainEvent);
                                }
                                catch (Exception exception)
                                {
                                    var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
                                    var logger = loggerFactory.CreateLogger("DomainEventsProcessor");

                                    logger.LogError(exception, $"Error has accrued while execution domain event {domainEvent.GetType()}. the message is: {exception.Message}");
                                }
                            });
                        }

                        entity.ClearDomainEvents();
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
