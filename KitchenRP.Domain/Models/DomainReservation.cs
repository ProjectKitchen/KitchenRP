using System.Collections.Generic;
using KitchenRP.DataAccess;
using KitchenRP.DataAccess.Models;
using NodaTime;

namespace KitchenRP.Domain.Models
{
    public class DomainReservation
    {
        public long Id { get; set; }
        public Interval ReservedFor { get; set; }
        public DomainUser? Owner { get; set; }
        public DomainResource? ReservedResource { get; set; }
        public DomainReservationStatus? Status { get; set; }
        public bool AllowNotifications { get; set; }
    }
}