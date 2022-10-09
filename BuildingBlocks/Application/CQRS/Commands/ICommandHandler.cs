using MediatR;

namespace BuildingBlocks.Application.CQRS.Commands
{
    public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
    }
}
