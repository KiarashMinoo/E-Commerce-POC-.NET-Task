using BuildingBlocks.Application.Data;

namespace BuildingBlocks.Infrastructure
{
    public abstract class ContextBase : IContext
    {
        public abstract Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
