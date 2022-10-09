using Api.Dto.Products;
using Application.CQRS.Products.Commands.Create;
using Application.CQRS.Products.Commands.Delete;
using Application.CQRS.Products.Commands.Update;
using Application.CQRS.Products.Queries.ListAll;
using Application.CQRS.Products.Queries.Retrieve;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly string[] AllowedContentTypes = new[] { "image/jpg", "image/jpeg", "image/pjpeg", "image/gif", "image/x-png", "image/png" };

        [HttpPost]
        [Authorize]
        public Task<CreateProductCommandResultDto> CreateAsync([FromServices] IMediator mediator, [FromForm] ProductUpsertDto dto, CancellationToken cancellationToken = default)
        {
            if (dto.Image is null)
                throw new ArgumentNullException(nameof(dto.Image));

            if (!AllowedContentTypes.Contains(dto.Image.ContentType))
                throw new InvalidOperationException("the ContentType is invalid");

            return mediator.Send(new CreateProductCommand(dto.Name, dto.Quantity, dto.Price, dto.Image.OpenReadStream()), cancellationToken);
        }

        [HttpPut("{id:Guid}")]
        [Authorize]
        public Task<UpdateProductCommandResultDto> UpdateAsync([FromServices] IMediator mediator, [FromRoute] Guid id, [FromForm] ProductUpsertDto dto, CancellationToken cancellationToken = default)
        {
            if (dto.Image is not null && !AllowedContentTypes.Contains(dto.Image.ContentType))
                throw new InvalidOperationException("the ContentType is invalid");

            return mediator.Send(new UpdateProductCommand(id, dto.Name, dto.Quantity, dto.Price, dto.Image?.OpenReadStream()), cancellationToken);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize]
        public Task<DeleteProductCommandResultDto> DeleteAsync([FromServices] IMediator mediator, [FromRoute] Guid id, CancellationToken cancellationToken = default)
            => mediator.Send(new DeleteProductCommand(id), cancellationToken);

        [HttpGet("{id:Guid}")]
        [Authorize]
        public Task<RetrieveProductQueryResultDto?> RetrieveAsync([FromServices] IMediator mediator, [FromRoute] Guid id, CancellationToken cancellationToken = default)
            => mediator.Send(new RetrieveProductQuery(id), cancellationToken);

        [HttpGet]
        [Authorize]
        public Task<IEnumerable<ListAllProductQueryResultDto>> ListAllAsync([FromServices] IMediator mediator, CancellationToken cancellationToken = default)
            => mediator.Send(new ListAllProductQuery(), cancellationToken);
    }
}
