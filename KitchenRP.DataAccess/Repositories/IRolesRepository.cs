using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;

namespace KitchenRP.DataAccess.Repositories
{
    public interface IRolesRepository
    {
        Task<UserRole> GetByRole(string role);
    }
}