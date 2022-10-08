using Application.Data;
using BuildingBlocks.Infrastructure;

namespace Infrastructure
{
    internal class UnitOfWork : UnitOfWorkBase
    {
        private readonly IPostgreSqlContext context;

        public UnitOfWork(IPostgreSqlContext context) => this.context = context;

        public override Task<int> CommitAsync(CancellationToken cancellationToken = default) => context.SaveChangesAsync(cancellationToken);
    }
}