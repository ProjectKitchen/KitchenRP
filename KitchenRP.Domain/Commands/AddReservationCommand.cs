using NodaTime;

namespace KitchenRP.Domain.Commands
{
    public class AddReservationCommand
    {
        public Instant StartTime { get; set; }

        public Instant EndTime { get; set; }

        public long UserId { get; set; }

        public long ResourceId { get; set; }

        public bool AllowNotifications { get; set; }
    }
}
