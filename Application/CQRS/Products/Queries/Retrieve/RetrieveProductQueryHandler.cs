using AutoMapper;
using BuildingBlocks.Application.CQRS.Queries;
using Domain.Products;

namespace Application.CQRS.Products.Queries.Retrieve
{
    internal class RetrieveProductQueryHandler : IQueryHandler<RetrieveProductQuery, RetrieveProductQueryResultDto?>
    {
        private readonly IMongoDbProductRepository repository;
        private readonly IMapper mapper;

        public RetrieveProductQueryHandler(IMongoDbProductRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<RetrieveProductQueryResultDto?> Handle(RetrieveProductQuery request, CancellationToken cancellationToken)
        {
            var customer = await repository.RetrieveAsync(request.Id, cancellationToken);
            return mapper.Map<RetrieveProductQueryResultDto?>(customer);
        }
    }
}
