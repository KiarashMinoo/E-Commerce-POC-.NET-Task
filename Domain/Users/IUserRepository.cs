using BuildingBlocks.Domain;
using Domain.Customers.BusinessRules.UserUserNameMustBeUnique;

namespace Domain.Users
{
    public interface IUserRepository : IRepository<User>, IUserUserNameMustBeUniqueHandler
    {
        Task<User> AddAsync(User user, CancellationToken cancellationToken = default);
        Task<User?> RetrieveAsync(Guid id, CancellationToken cancellationToken = default);
        Task<User?> RetrieveByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    }
}
