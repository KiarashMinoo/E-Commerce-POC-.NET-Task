using MediatR;

namespace BuildingBlocks.Application.CQRS.Commands
{
    public interface ICommand : IBaseRequest
    {
    }

    public interface ICommand<out TResult> : ICommand, IRequest<TResult>
    {
    }
}
