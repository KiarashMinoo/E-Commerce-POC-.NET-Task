namespace Application.Services.Token
{
    public interface ITokenService
    {
        string GenerateJSONWebToken(Guid userId, string userName, Guid customerId);
    }
}
