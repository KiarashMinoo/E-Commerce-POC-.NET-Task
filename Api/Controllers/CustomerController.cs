using Api.Dto.Customers;
using Application.CQRS.Customers.Commands.Create;
using Application.CQRS.Customers.Commands.Delete;
using Application.CQRS.Customers.Commands.Update;
using Application.CQRS.Customers.Queries.ListAll;
using Application.CQRS.Customers.Queries.Retrieve;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public Task<CreateCustomerCommandResultDto> CreateAsync([FromServices] IMediator mediator, [FromBody] CustomerUpsertDto dto, CancellationToken cancellationToken = default)
            => mediator.Send(new CreateCustomerCommand(dto.FullName, dto.EMail, dto.Cell), cancellationToken);

        [HttpPut("{id:Guid}")]
        [Authorize]
        public Task<UpdateCustomerCommandResultDto> UpdateAsync([FromServices] IMediator mediator, [FromRoute] Guid id, [FromBody] CustomerUpsertDto dto, CancellationToken cancellationToken = default)
            => mediator.Send(new UpdateCustomerCommand(id, dto.FullName, dto.EMail, dto.Cell), cancellationToken);

        [HttpPut("{id:Guid}")]
        [Authorize]
        public Task<DeleteCustomerCommandResultDto> DeleteAsync([FromServices] IMediator mediator, [FromRoute] Guid id, CancellationToken cancellationToken = default)
            => mediator.Send(new DeleteCustomerCommand(id), cancellationToken);

        [HttpGet("{id:Guid}")]
        [Authorize]
        public Task<RetrieveCustomerQueryResultDto?> RetrieveAsync([FromServices] IMediator mediator, [FromRoute] Guid id, CancellationToken cancellationToken = default)
            => mediator.Send(new RetrieveCustomerQuery(id), cancellationToken);

        [HttpGet]
        [Authorize]
        public Task<IEnumerable<ListAllCustomerQueryResultDto>> ListAllAsync([FromServices] IMediator mediator, CancellationToken cancellationToken = default)
            => mediator.Send(new ListAllCustomerQuery(), cancellationToken);
    }
}
