using BuildingBlocks.Application.CQRS.Queries;

namespace Application.CQRS.Products.Queries.Retrieve
{
    public class RetrieveProductQuery : IQuery<RetrieveProductQueryResultDto?>
    {
        public Guid Id { get; }

        public RetrieveProductQuery(Guid id)
        {
            Id = id;
        }
    }
}
