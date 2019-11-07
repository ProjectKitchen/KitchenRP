namespace KitchenRP.Domain.Models
{
    public static class Roles
    {
        public const string Admin = "admin";
        public const string Moderator = "moderator,admin";
        public const string User = "user,moderator,admin";
    }
}