using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KitchenRP.DataAccess.Models;
using NodaTime;

namespace KitchenRP.DataAccess.Queries
{
    public class ReservationQuery
    {
        public User Owner { get; set; }
        public Resource Resource { get; set; }
        public Instant From { get; set; }
        public Instant To { get; set; }
        public IEnumerable<ReservationStatus> Statuses { get; set; }

        public ReservationQuery ForOwner(User owner)
        {
            this.Owner = owner;
            return this;
        }

        public ReservationQuery ForResource(Resource resource)
        {
            this.Resource = resource;
            return this;
        }

        public ReservationQuery Between(Instant from, Instant to)
        {
            From = from;
            To = to;
            return this;
        }

        public ReservationQuery WithStatuses(IEnumerable<ReservationStatus> statuses)
        {
            Statuses = statuses;
            return this;
        }

        internal IQueryable<Reservation> RunQuery(IQueryable<Reservation> reservations, KitchenRpContext _context)
        {
            if (Owner != null)
                reservations = reservations.Where(r => r.Owner.Id == Owner.Id);
            if (Resource != null)
                reservations = reservations.Where(r => r.ReservedResource.Id == Resource.Id);
            if (Statuses != null)
            {
                foreach (var status in Statuses)
                {
                    reservations = reservations.Where(r => r.)
                }
            }
                return reservations;
        }
    }
}
