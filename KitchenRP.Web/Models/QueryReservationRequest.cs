using System;
using NodaTime;

namespace KitchenRP.Web.Models
{
    public class QueryReservationRequest
    {
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public long? UserId { get; set; }
        public long? ResourceId { get; set; }
        public string? Statuses { get; set; }
        public string[] StatusList => string.IsNullOrWhiteSpace(Statuses)
            ? new string[0]
            : Statuses.Split(",");
    }
}