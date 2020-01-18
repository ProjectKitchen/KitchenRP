using NodaTime;

namespace KitchenRP.Domain.Commands
{
    public class QueryReservationCommand
    {
        public Instant? StartTime { get; set; }
        public Instant? EndTime { get; set; }
        public long? UserId { get; set; }
        public long? ResourceId { get; set; }
        public string[] StatusList { get; set; }
    }
}