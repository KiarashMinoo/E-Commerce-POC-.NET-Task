using MediatR;

namespace BuildingBlocks.Application.CQRS.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}