namespace Domain.Customers.BusinessRules.CustomerCellMustBeUnique
{
    public interface ICustomerCellMustBeUniqueHandler
    {
        bool CustomerExistsByCell(Guid customerId, string cell);
    }
}
