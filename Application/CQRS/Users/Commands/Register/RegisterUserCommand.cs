using BuildingBlocks.Application.CQRS.Commands;

namespace Application.CQRS.Users.Commands.Register
{
    public class RegisterUserCommand : ICommand<RegisterUserCommandResultDto>
    {
        public string UserName { get; }
        public string Password { get; }
        public string EMail { get; }
        public string Cell { get; }
        public string FullName { get; }

        public RegisterUserCommand(string userName, string password, string eMail, string cell, string fullName)
        {
            UserName = userName;
            Password = password;
            EMail = eMail;
            Cell = cell;
            FullName = fullName;
        }
    }
}
