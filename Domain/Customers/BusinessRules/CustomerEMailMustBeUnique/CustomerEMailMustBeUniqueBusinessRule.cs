using BuildingBlocks.Domain.BusinessRules;

namespace Domain.Customers.BusinessRules.CustomerEMailMustBeUnique
{
    internal class CustomerEMailMustBeUniqueBusinessRule : IBusinessRule
    {
        private readonly ICustomerEMailMustBeUniqueHandler handler;
        private readonly Guid customerId;
        private readonly string email;

        public string Message => $"The email addresses has already exists in database.";

        public CustomerEMailMustBeUniqueBusinessRule(ICustomerEMailMustBeUniqueHandler handler, Guid customerId, string email)
        {
            this.handler = handler;
            this.customerId = customerId;
            this.email = email;
        }

        public bool IsBroken() => handler.CustomerExistsByEMail(customerId, email);
    }
}
