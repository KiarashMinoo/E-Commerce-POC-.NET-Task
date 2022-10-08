using AutoMapper;
using BuildingBlocks.Application.CQRS.Queries;
using Domain.Customers;

namespace Application.CQRS.Customers.Queries.ListAll
{
    internal class ListAllCustomerQueryHandler : IQueryHandler<ListAllCustomerQuery, IEnumerable<ListAllCustomerQueryResultDto>>
    {
        private readonly IMongoDbCustomerRepository repository;
        private readonly IMapper mapper;

        public ListAllCustomerQueryHandler(IMongoDbCustomerRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ListAllCustomerQueryResultDto>> Handle(ListAllCustomerQuery request, CancellationToken cancellationToken)
        {
            var customers = await repository.ListAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<ListAllCustomerQueryResultDto>>(customers);
        }
    }
}
