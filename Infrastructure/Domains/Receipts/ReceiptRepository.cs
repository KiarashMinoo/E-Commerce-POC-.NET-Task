using Application.Data;
using Domain.Receipts;

namespace Infrastructure.Domains.Receipts
{
    internal class ReceiptRepository : IReceiptRepository
    {
        private readonly IPostgreSqlContext context;

        public ReceiptRepository(IPostgreSqlContext context)
        {
            this.context = context;
        }

        public async Task<Receipt> AddAsync(Receipt user, CancellationToken cancellationToken = default)
        {
            var entry = await context.Receipts.AddAsync(user, cancellationToken);
            return entry.Entity;
        }
    }
}
