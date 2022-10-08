using BuildingBlocks.Application.CQRS.Queries;

namespace Application.CQRS.Customers.Queries.Retrieve
{
    public class RetrieveCustomerQuery : IQuery<RetrieveCustomerQueryResultDto?>
    {
        public Guid Id { get; }

        public RetrieveCustomerQuery(Guid id)
        {
            Id = id;
        }
    }
}
