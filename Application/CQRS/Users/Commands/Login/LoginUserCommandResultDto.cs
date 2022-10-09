namespace Application.CQRS.Users.Commands.Login
{
    public class LoginUserCommandResultDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
