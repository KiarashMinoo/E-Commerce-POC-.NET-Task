using BuildingBlocks.Application.CQRS.Commands;

namespace Application.CQRS.Users.Commands.Login
{
    public class LoginUserCommand : ICommand<LoginUserCommandResultDto>
    {
        public string UserName { get; }
        public string Password { get; }

        public LoginUserCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
