using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;

namespace KitchenRP.DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<User> FindById(long id);
        Task<User> FindBySub(string sub);
        Task<User> CreateNewUser(string sub, string role, string email);

        Task<bool> Exists(string sub);

        Task<User> UpdateUser(User u);
    }
}