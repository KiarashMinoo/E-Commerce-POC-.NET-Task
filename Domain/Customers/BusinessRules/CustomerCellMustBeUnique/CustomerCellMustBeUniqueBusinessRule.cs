using BuildingBlocks.Domain.BusinessRules;

namespace Domain.Customers.BusinessRules.CustomerCellMustBeUnique
{
    internal class CustomerCellMustBeUniqueBusinessRule : IBusinessRule
    {
        private readonly ICustomerCellMustBeUniqueHandler handler;
        private readonly Guid customerId;
        private readonly string cell;

        public string Message => $"The cell phone has already exists in database.";

        public CustomerCellMustBeUniqueBusinessRule(ICustomerCellMustBeUniqueHandler handler, Guid customerId, string cell)
        {
            this.handler = handler;
            this.customerId = customerId;
            this.cell = cell;
        }

        public bool IsBroken() => handler.CustomerExistsByCell(customerId, cell);
    }
}
