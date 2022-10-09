using BuildingBlocks.Domain.BusinessRules;

namespace Domain.Customers.BusinessRules.UserUserNameMustBeUnique
{
    internal class UserUserNameMustBeUniqueBusinessRule : IBusinessRule
    {
        private readonly IUserUserNameMustBeUniqueHandler handler;
        private readonly Guid userId;
        private readonly string userName;

        public string Message => $"The username has already exists in database.";

        public UserUserNameMustBeUniqueBusinessRule(IUserUserNameMustBeUniqueHandler handler, Guid userId, string userName)
        {
            this.handler = handler;
            this.userId = userId;
            this.userName = userName;
        }

        public bool IsBroken() => handler.UserExistsByUserName(userId, userName);
    }
}
