using System;
using System.Threading.Tasks;
using KitchenRP.Domain.Models;

namespace KitchenRP.Web.Models
{
    public static class Mapper
    {
        public static UserResponse Map(DomainUser u)
            => new UserResponse(u.Id, u.Sub, u.Email, u.AllowNotifications);
    }
}