using BuildingBlocks.Application.CQRS.Queries;

namespace Application.CQRS.Customers.Queries.ListAll
{
    public class ListAllCustomerQuery : IQuery<IEnumerable<ListAllCustomerQueryResultDto>>
    {
    }
}
