namespace Domain.Customers.BusinessRules.CustomerEMailMustBeUnique
{
    public interface ICustomerEMailMustBeUniqueHandler
    {
        bool CustomerExistsByEMail(Guid customerId, string eMail);
    }
}
