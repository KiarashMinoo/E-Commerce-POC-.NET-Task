using BuildingBlocks.Domain;

namespace Domain.Receipts
{
    public interface IReceiptRepository : IRepository<Receipt>
    {
        Task<Receipt> AddAsync(Receipt receipt, CancellationToken cancellationToken = default);
    }
}
