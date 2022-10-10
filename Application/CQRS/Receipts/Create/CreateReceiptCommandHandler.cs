using BuildingBlocks.Application.CQRS.Commands;
using BuildingBlocks.Domain;
using Domain.Customers;
using Domain.Products;
using Domain.Receipts;
using Microsoft.AspNetCore.Http;

namespace Application.CQRS.Receipts.Create
{
    internal class CreateReceiptCommandHandler : ICommandHandler<CreateReceiptCommand, CreateReceiptCommandResultDto>
    {
        private readonly IReceiptRepository repository;
        private readonly IPostgreSqlProductRepository productRepository;
        private readonly IPostgreSqlCustomerRepository customerRepository;
        private readonly HttpContext httpContext;

        public CreateReceiptCommandHandler(IReceiptRepository repository, IPostgreSqlProductRepository productRepository, IPostgreSqlCustomerRepository customerRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.repository = repository;
            this.productRepository = productRepository;
            this.customerRepository = customerRepository;
            httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<CreateReceiptCommandResultDto> Handle(CreateReceiptCommand request, CancellationToken cancellationToken)
        {
            var customerId = httpContext.User.Claims.First(a => a.Type == "CustomerId").Value;

            var product = await productRepository.RetrieveAsync(request.ProductId, cancellationToken) ?? throw new EntityNotFoundException<Product>();
            var customer = await customerRepository.RetrieveAsync(Guid.Parse(customerId), cancellationToken) ?? throw new EntityNotFoundException<Customer>();

            product = product.DecreaseQuantity(request.Quantity);            

            var receipt = new Receipt(customer.Id, product.Id, request.Quantity, product.Price);
            _ = await repository.AddAsync(receipt, cancellationToken);

            var rtn = new CreateReceiptCommandResultDto() { Id = receipt.Id };
            rtn.SetEntity(product);
            return rtn;
        }
    }
}
