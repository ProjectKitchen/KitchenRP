namespace KitchenRP.Web.Models
{
    public class UserResponse
    {
        public UserResponse(long id, string sub, string email, bool allowNotifications)
        {
            Id = id;
            Sub = sub;
            Email = email;
            AllowNotifications = allowNotifications;
        }

        public long Id { get; }
        public string Sub { get; }
        public string Email { get; }
        public bool AllowNotifications { get; }
    }
}