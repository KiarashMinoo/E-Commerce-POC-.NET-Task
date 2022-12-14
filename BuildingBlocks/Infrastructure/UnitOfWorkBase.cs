using BuildingBlocks.Application.Data;

namespace BuildingBlocks.Infrastructure
{
    public abstract class UnitOfWorkBase : IUnitOfWork, IDisposable
    {
        ~UnitOfWorkBase()
        {
            Dispose(false);
        }

        public abstract Task<int> CommitAsync(CancellationToken cancellationToken = default);

        protected internal virtual Task BeginTransactionAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

        protected internal virtual Task RollbackTransactionAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

        protected internal virtual Task CommitTransactionAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}