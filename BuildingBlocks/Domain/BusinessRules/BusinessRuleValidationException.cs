using System.Net;

namespace BuildingBlocks.Domain.BusinessRules
{
    public class BusinessRuleValidationException : HttpRequestException
    {
        public IBusinessRule BrokenRule { get; }

        public string Details { get; }

        public BusinessRuleValidationException(IBusinessRule brokenRule) : base(brokenRule.Message, null, HttpStatusCode.UnprocessableEntity)
        {
            BrokenRule = brokenRule;
            Details = brokenRule.Message;
        }

        public override string ToString() => $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";
    }
}
