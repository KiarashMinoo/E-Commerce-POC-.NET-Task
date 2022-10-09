using Api.Dto.Users;
using Application.CQRS.Users.Commands.Login;
using Application.CQRS.Users.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CredentialController : ControllerBase
    {
        [HttpPost("register")]
        public Task<RegisterUserCommandResultDto> RegisterAsync([FromServices] IMediator mediator, [FromBody] RegisterUserDto dto, CancellationToken cancellationToken = default)
            => mediator.Send(new RegisterUserCommand(dto.UserName, dto.Password, dto.EMail, dto.Cell, dto.FullName), cancellationToken);

        [HttpPost("login")]
        public Task<LoginUserCommandResultDto> LoginAsync([FromServices] IMediator mediator, [FromBody] LoginUserDto dto, CancellationToken cancellationToken = default)
            => mediator.Send(new LoginUserCommand(dto.UserName, dto.Password), cancellationToken);
    }
}
