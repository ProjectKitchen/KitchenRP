using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;

namespace KitchenRP.DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<User> UserById(long id);
        Task<User> UserBySub(string sub);
    }
}