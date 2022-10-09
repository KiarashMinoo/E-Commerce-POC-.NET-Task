using BuildingBlocks.Domain;
using Domain.Customers;
using Domain.Customers.BusinessRules.UserUserNameMustBeUnique;

namespace Domain.Users
{
    public class User : EntityBase, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string UserName { get; private set; } = null!;
        public string Password { get; private set; } = null!;
        public string Salt { get; private set; } = null!;
        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; } = null!;

        private User() => Id = Guid.NewGuid();

        public User(string userName, string password, string salt, Guid customerId, IUserUserNameMustBeUniqueHandler userNameHandler) : this()
        {
            CheckBusinessRule(new UserUserNameMustBeUniqueBusinessRule(userNameHandler, Id, userName));

            UserName = userName;
            Password = password;
            Salt = salt;
            CustomerId = customerId;
        }

        public User Update(string userName, string password, string salt, Guid customerId, IUserUserNameMustBeUniqueHandler userNameHandler)
        {
            CheckBusinessRule(new UserUserNameMustBeUniqueBusinessRule(userNameHandler, Id, userName));

            UserName = userName;
            Password = password;
            Salt = salt;
            CustomerId = customerId;

            return this;
        }
    }
}
