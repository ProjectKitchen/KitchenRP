using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace KitchenRP.DataAccess.Queries
{
    public class ReservationQuery
    {
        private User Owner { get; set; }
        private Resource Resource { get; set; }
        private Instant? From { get; set; }
        private Instant? To { get; set; }
        private IEnumerable<ReservationStatus> Statuses { get; set; }

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

        public ReservationQuery CollideWith(Instant from, Instant to)
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

        internal IQueryable<Reservation> RunQuery(KitchenRpContext ctx)
        {
            var reservations = ctx.Reservations.Select(r => r);
            
            if (Statuses != null)
            {
                var reservations1 = reservations;
                var r =
                    from innerst in ctx.StatusChanges
                    group innerst by innerst.Reservation.Id
                    into g
                    select new {change = g.Min(sc => sc.ChangedAt), id = g.Key}
                    into min

                    from res in reservations1
                    join statuses in ctx.StatusChanges on res.Id equals statuses.Reservation.Id
                    where statuses.ChangedAt == min.change && res.Id == min.id
                    select new TempResult {R = res, S = statuses};

                var pred = PredicateBuilder.False<TempResult>();
                
                foreach (var status in Statuses)
                {
                    pred = pred.Or(sc => sc.S.CurrentStatus.Id == status.Id);
                }

                reservations = r.Where(pred).Select(t => t.R);
            }
            
            if (Owner != null)
            {
                reservations = reservations.Where(r => r.Owner.Id == Owner.Id);
            }

            if (Resource != null)
            {
                reservations = reservations.Where(r => r.ReservedResource.Id == Resource.Id);
            }

            if (From != null && To != null)
            {
                reservations = reservations
                    .Where(r => r.StartTime >= From  && r.EndTime <= To 
                                ||
                                r.EndTime > From
                                ||
                                r.StartTime < To
                                || 
                                r.StartTime <= From && r.EndTime >= To
                    );
            }
            
           
            return reservations;
        }
        private class TempResult
        {
            public Reservation R { get; set; }
            public StatusChange S { get; set; }
        }
        
    }

    

}

