using AutoMapper;
using BuildingBlocks.Application.CQRS.Queries;
using Domain.Customers;

namespace Application.CQRS.Customers.Queries.Retrieve
{
    internal class RetrieveCustomerQueryHandler : IQueryHandler<RetrieveCustomerQuery, RetrieveCustomerQueryResultDto?>
    {
        private readonly IMongoDbCustomerRepository repository;
        private readonly IMapper mapper;

        public RetrieveCustomerQueryHandler(IMongoDbCustomerRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<RetrieveCustomerQueryResultDto?> Handle(RetrieveCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await repository.RetrieveAsync(request.Id, cancellationToken);
            return mapper.Map<RetrieveCustomerQueryResultDto?>(customer);
        }
    }
}
