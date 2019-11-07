using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KitchenRP.DataAccess.Models
{
    public class User
    {
        public User(
            long id,
            string sub,
            UserRole role,
            string email,
            bool allowNotifications)
        {
            Id = id;
            Sub = sub;
            Role = role;
            Email = email;
            AllowNotifications = allowNotifications;
        }

        private User(long id, string sub, string email, bool allowNotifications)
        {
            Id = id;
            Sub = sub;
            Email = email;
            AllowNotifications = allowNotifications;
        }

        public long Id { get; private set; }
        public string Sub { get; private set; }
        public UserRole Role { get; private set; }
        public string Email { get; private set; }
        public bool AllowNotifications { get; private set; }
    }

    internal class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Sub)
                .HasMaxLength(8)
                .IsFixedLength();
        }
    }
}