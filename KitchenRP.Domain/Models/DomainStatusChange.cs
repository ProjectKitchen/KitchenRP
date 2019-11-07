using NodaTime;

namespace KitchenRP.Domain.Models
{
    public class DomainStatusChange
    {
        public DomainStatusChange(DomainReservationStatus? previousStatus, DomainReservationStatus? currentStatus,
            string reason, Instant changedAt, DomainUser? changedBy)
        {
            PreviousStatus = previousStatus;
            CurrentStatus = currentStatus;
            Reason = reason;
            ChangedAt = changedAt;
            ChangedBy = changedBy;
        }

        public DomainReservationStatus? PreviousStatus { get; }
        public DomainReservationStatus? CurrentStatus { get; }
        public string Reason { get; }
        public Instant ChangedAt { get; }
        public DomainUser? ChangedBy { get; }
    }
}