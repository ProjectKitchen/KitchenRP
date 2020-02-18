namespace KitchenRP.Web.Models
{
    public class UserResponse
    {
        public UserResponse(long id, string sub, string email, string role, bool allowNotifications)

        {
            Id = id;
            Sub = sub;
            Email = email;
            Role = role;
            AllowNotifications = allowNotifications;
            Role = role;
        }

        public long Id { get; }
        public string Sub { get; }
        public string Email { get; }
        public string Role { get; }
        public bool AllowNotifications { get; }
    }
}