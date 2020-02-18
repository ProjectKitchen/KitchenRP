using NodaTime;

namespace KitchenRP.Domain.Models
{
    public class DomainStatusChange
    {
        public DomainReservationStatus? PreviousStatus { get; set; }
        public DomainReservationStatus? CurrentStatus { get; set; }
        public string Reason { get; set; }
        public Instant ChangedAt { get; set; }
        public DomainUser? ChangedBy { get; set; }
    }
}