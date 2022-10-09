using Application.Data;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Domains.Users
{
    internal class UserRepository : IUserRepository
    {
        private readonly IPostgreSqlContext context;

        public UserRepository(IPostgreSqlContext context)
        {
            this.context = context;
        }

        public async Task<User> AddAsync(User user, CancellationToken cancellationToken = default)
        {
            var entry = await context.Users.AddAsync(user, cancellationToken);
            return entry.Entity;
        }

        public Task<User?> RetrieveAsync(Guid id, CancellationToken cancellationToken = default)
            => context.Users.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        public Task<User?> RetrieveByUserNameAsync(string userName, CancellationToken cancellationToken = default)
            => context.Users.FirstOrDefaultAsync(a => a.UserName == userName, cancellationToken);

        public bool UserExistsByUserName(Guid userId, string userName)
            => context.Users.Any(a => a.UserName == userName && a.Id != userId);
    }
}
