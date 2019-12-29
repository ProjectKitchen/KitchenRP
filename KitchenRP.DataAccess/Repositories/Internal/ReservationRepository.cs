using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;
using KitchenRP.DataAccess.Queries;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace KitchenRP.DataAccess.Repositories.Internal
{
    public class ReservationRepository : IReservationRepository
    {
        private KitchenRpContext _ctx;

        public ReservationRepository(KitchenRpContext ctx)
        {
            _ctx = ctx;
        }

        public ValueTask<Reservation> FindById(long id)
        {
            return _ctx.Reservations.FindAsync(id);
        }

        public async Task<Reservation> CreateNewReservation(Instant start, Instant end, Resource resource, User owner,
            bool allowNotifications)
        {
            var reservation = new Reservation
            {
                StartTime = start, EndTime = end, Owner = owner, ReservedResource = resource,
                AllowNotifications = allowNotifications
            };
            _ctx.Reservations.Add(reservation);
            await _ctx.SaveChangesAsync();
            return reservation;
        }

        public Task<List<Reservation>> ByUser(User u)
        {
            return _ctx.Reservations.Where(r => r.Owner.Id == u.Id).ToListAsync();
        }

        public Task<List<Reservation>> All()
        {
            return _ctx.Reservations.ToListAsync();
        }

        public Task<List<Reservation>> Query(ReservationQuery query)
        {
            return query.RunQuery(_ctx).ToListAsync();
        }

        public Task<ReservationStatus> CurrentStatus(Reservation r)
        {
            return _ctx.StatusChanges
                .Where(sc => sc.Reservation.Id == r.Id)
                .OrderByDescending(sc => sc.ChangedAt)
                .Select(sc => sc.CurrentStatus)
                .FirstOrDefaultAsync();
        }

        public async Task<StatusChange> CreateNewStatusChange(string reason, Reservation reservation, User changedBy,
            ReservationStatus newStatus)
        {
            var oldStatus = await CurrentStatus(reservation);
            var statusChange = new StatusChange
            {
                Id = null,
                PreviousStatus = oldStatus,
                CurrentStatus = newStatus,
                Reason = reason,
                ChangedAt = SystemClock.Instance.GetCurrentInstant(),
                Reservation = reservation,
                ChangedBy = changedBy
            };
            _ctx.StatusChanges.Add(statusChange);
            await _ctx.SaveChangesAsync();
            return statusChange;
        }
    }
}