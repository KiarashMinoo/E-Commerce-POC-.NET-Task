using AloNBS.BuildingBlocks.Application.Attributes;
using BuildingBlocks.Application.CQRS.Commands;
using BuildingBlocks.Application.Data;
using MediatR;

namespace BuildingBlocks.Infrastructure.Mediatr
{
    internal class CommandHandlerUnitOfWorkDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRequestHandler<TRequest, TResponse> decorated;

        public CommandHandlerUnitOfWorkDecorator(IUnitOfWork unitOfWork, IRequestHandler<TRequest, TResponse> decorated)
        {
            this.unitOfWork = unitOfWork;
            this.decorated = decorated;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken = default)
        {
            Task TransactionHandleAsync(Func<UnitOfWorkBase, Task> funcToExecute)
                => unitOfWork is UnitOfWorkBase unitOfWorkBase ? funcToExecute.Invoke(unitOfWorkBase) : Task.CompletedTask;

            var transactional = typeof(TRequest).GetCustomAttributes(typeof(TransactionalAttribute), true).Any();

            if (transactional)
                await TransactionHandleAsync(uow => uow.BeginTransactionAsync());

            try
            {
                var rtn = await decorated.Handle(request, cancellationToken);

                if (request is ICommand)
                    await unitOfWork.CommitAsync(cancellationToken);

                if (transactional)
                    await TransactionHandleAsync(uow => uow.CommitTransactionAsync());

                return rtn;
            }
            catch (Exception exception)
            {
                if (transactional)
                    await TransactionHandleAsync(uow => uow.RollbackTransactionAsync());

                throw;
            }
        }
    }
}
