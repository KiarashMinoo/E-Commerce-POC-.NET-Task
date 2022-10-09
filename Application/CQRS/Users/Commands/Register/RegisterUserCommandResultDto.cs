using BuildingBlocks.Application.CQRS.Commands;

namespace Application.CQRS.Users.Commands.Register
{
    public class RegisterUserCommandResultDto : EntityResult
    {
        public Guid Id { get; set; }
    }
}
