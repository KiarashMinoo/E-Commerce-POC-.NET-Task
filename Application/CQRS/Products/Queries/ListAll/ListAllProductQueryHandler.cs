using AutoMapper;
using BuildingBlocks.Application.CQRS.Queries;
using Domain.Products;

namespace Application.CQRS.Products.Queries.ListAll
{
    internal class ListAllProductQueryHandler : IQueryHandler<ListAllProductQuery, IEnumerable<ListAllProductQueryResultDto>>
    {
        private readonly IMongoDbProductRepository repository;
        private readonly IMapper mapper;

        public ListAllProductQueryHandler(IMongoDbProductRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ListAllProductQueryResultDto>> Handle(ListAllProductQuery request, CancellationToken cancellationToken)
        {
            var customers = await repository.ListAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<ListAllProductQueryResultDto>>(customers);
        }
    }
}
