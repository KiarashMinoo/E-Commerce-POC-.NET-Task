using BuildingBlocks.Application.CQRS.Queries;

namespace Application.CQRS.Products.Queries.ListAll
{
    public class ListAllProductQuery : IQuery<IEnumerable<ListAllProductQueryResultDto>>
    {
    }
}
