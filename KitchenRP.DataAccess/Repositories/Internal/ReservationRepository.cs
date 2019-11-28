using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;
using KitchenRP.DataAccess.Queries;
using Microsoft.EntityFrameworkCore;

namespace KitchenRP.DataAccess.Repositories.Internal
{
    public class ReservationRepository: IReservationRepository
    {
        private KitchenRpContext _ctx;

        public ReservationRepository(KitchenRpContext ctx)
        {
            _ctx = ctx;
        }

        public Task<Reservation> CreateNewReservation()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Reservation>> ByUser(User u)
        {
            return _ctx.Reservations.Where(r => r.Owner.Id == u.Id).ToListAsync();
        }

        public Task<List<Reservation>> All()
        {
            return _ctx.Reservations.ToListAsync();
        }

        public async Task<List<Reservation>> Query(ReservationQuery query)
        {

        }
    }
}
