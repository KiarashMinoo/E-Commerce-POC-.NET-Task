using Api.Dto.Receipts;
using Application.CQRS.Receipts.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReceiptController : ControllerBase
    {
        [HttpPost]
        public Task<CreateReceiptCommandResultDto> RegisterAsync([FromServices] IMediator mediator, [FromBody] ReceiptDto dto, CancellationToken cancellationToken = default)
            => mediator.Send(new CreateReceiptCommand(dto.ProductId, dto.Quantity), cancellationToken);
    }
}
