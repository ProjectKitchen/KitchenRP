using NodaTime;

namespace KitchenRP.Web.Models
{
    public class QueryReservationRequest
    {
        public Instant? StartTime { get; set; }
        public Instant? EndTime { get; set; }
        public long? UserId { get; set; }
        public long? ResourceId { get; set; }
        public string Statuses { get; set; }
        public string[] StatusList => string.IsNullOrWhiteSpace(Statuses)
            ? new string[0]
            : Statuses.Split(",");
    }
}