namespace KitchenRP.Domain.Models
{
    public class DomainUser
    {
        public DomainUser(long id, string sub, string role, string email, bool allowNotifications)
        {
            Id = id;
            Sub = sub;
            Role = role;
            Email = email;
            AllowNotifications = allowNotifications;
        }

        public long Id { get; }
        public string Sub { get; }
        public string Role { get; }
        public string Email { get; }
        public bool AllowNotifications { get; }
    }
}