using System;
using KitchenRP.DataAccess.Models;
using KitchenRP.Domain.Models;
using NodaTime;

namespace KitchenRP.Web.Models
{
    public class ReservationResponse
    {
        public long Id { get; set; }
        public String StartTime { get; set; }
        public String EndTime { get; set; }
        public UserResponse Owner { get; set; }
        public DomainResource ReservedResource { get; set; }
        // public HashSet<StatusChange> StatusChanges { get; set; }
        public bool AllowNotifications { get; set; }
    }
}