namespace KitchenRP.DataAccess.Models
{
    public class UserRole
    {
        public UserRole(long id, string roleName)
        {
            Id = id;
            RoleName = roleName;
        }

        public long Id { get; private set; }
        public string RoleName { get; private set; }
    }
}