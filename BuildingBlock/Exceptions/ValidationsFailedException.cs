using FluentValidation.Results;
using System.Net;

namespace BuildingBlocks.Exceptions
{
    public class ValidationsFailedException : HttpRequestException
    {
        public ValidationsFailedException(string message) : base(message, null, HttpStatusCode.UnprocessableEntity)
        {
        }

        public ValidationsFailedException(IEnumerable<ValidationFailure> errors) : base(string.Join(",\n", errors.Select(x => x.ErrorMessage)), null, HttpStatusCode.UnprocessableEntity)
        {
        }
    }
}
