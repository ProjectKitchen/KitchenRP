using System.Collections.Generic;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;
using KitchenRP.DataAccess.Queries;

namespace KitchenRP.DataAccess.Repositories
{
    public interface IReservationRepository
    {
        Task<Reservation> CreateNewReservation();

        Task<List<Reservation>> ByUser(User u);
        Task<List<Reservation>> All();

        Task<List<Reservation>> Query(ReservationQuery query);
    }
}
