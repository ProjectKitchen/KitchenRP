using System.Collections.Generic;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;

namespace KitchenRP.DataAccess.Repositories
{
    public interface IReservationStatusRepository
    {
        Task<List<ReservationStatus>> All();
        ValueTask<ReservationStatus> ById(long id);
        Task<ReservationStatus> ByStatus(string status);
    }
}