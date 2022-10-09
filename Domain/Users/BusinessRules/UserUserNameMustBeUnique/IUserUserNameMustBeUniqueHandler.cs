namespace Domain.Customers.BusinessRules.UserUserNameMustBeUnique
{
    public interface IUserUserNameMustBeUniqueHandler
    {
        bool UserExistsByUserName(Guid userId, string userName);
    }
}
