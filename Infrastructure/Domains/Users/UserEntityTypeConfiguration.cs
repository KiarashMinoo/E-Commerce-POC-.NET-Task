using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Domains.Users
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.UserName);
            builder.Property(a => a.Password);
            builder.Property(a => a.Salt);

            builder.HasOne(a => a.Customer).WithMany().HasForeignKey(a => a.CustomerId);

            builder.HasIndex(a => a.UserName).IsUnique();
        }
    }
}
