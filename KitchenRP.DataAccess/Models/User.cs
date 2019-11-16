using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KitchenRP.DataAccess.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Sub { get; set; }
        public UserRole Role { get; set; }
        public string Email { get; set; }
        public bool AllowNotifications { get; set; }
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