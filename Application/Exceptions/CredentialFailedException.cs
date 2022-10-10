using System.Net;

namespace Application.Exceptions
{
    public class CredentialFailedException : HttpRequestException
    {
        public CredentialFailedException() : base("login failed", null, HttpStatusCode.Forbidden)
        {
        }
    }
}
