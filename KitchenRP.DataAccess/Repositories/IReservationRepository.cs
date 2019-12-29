using System.Collections.Generic;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;
using KitchenRP.DataAccess.Queries;
using NodaTime;

namespace KitchenRP.DataAccess.Repositories
{
    public interface IReservationRepository
    {
        Task<Reservation> CreateNewReservation(Instant start, Instant end, Resource resource, User owner,
            bool allowNotifications);

        ValueTask<Reservation> FindById(long id);
        
        Task<List<Reservation>> ByUser(User u);
        Task<List<Reservation>> All();

        Task<List<Reservation>> Query(ReservationQuery query);

        Task<StatusChange> CreateNewStatusChange(string reason, Reservation reservation, User changedBy,
            ReservationStatus newStatus);
    }
}
