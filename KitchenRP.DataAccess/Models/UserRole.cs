using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KitchenRP.DataAccess.Models
{
    public class UserRole
    {
        public UserRole(long id, string role)
        {
            Id = id;
            Role = role;
        }

        public long Id { get; private set; }
        public string Role { get; private set; }
    }
}