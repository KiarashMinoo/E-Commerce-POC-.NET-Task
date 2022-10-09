using Application.Exceptions;
using Application.Helpers;
using Application.Services.Token;
using BuildingBlocks.Application.CQRS.Commands;
using Domain.Users;
using Domain.Users.SharedKernel;

namespace Application.CQRS.Users.Commands.Login
{
    internal class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, LoginUserCommandResultDto>
    {
        private readonly IUserRepository repository;
        private readonly ITokenService tokenService;

        public LoginUserCommandHandler(IUserRepository repository, ITokenService tokenService)
        {
            this.repository = repository;
            this.tokenService = tokenService;
        }

        public async Task<LoginUserCommandResultDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await repository.RetrieveByUserNameAsync(request.UserName, cancellationToken) ?? throw new CredentialFailedException();
            var password = user.Password.Decrypt(KeyProvider.Key, user.Salt);
            if (!password.Equals(request.Password))
                throw new CredentialFailedException();

            var token = tokenService.GenerateJSONWebToken(user.Id, user.UserName, user.CustomerId);

            return new LoginUserCommandResultDto() { Id = user.Id, UserName = user.UserName, Token = token };
        }
    }
}
