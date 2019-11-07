using NodaTime;

namespace KitchenRP.Domain.Models
{
    public class DomainReservation
    {
        public DomainReservation(long id, Instant startTime, Instant endTime, DomainUser? owner,
            DomainResource? reservedResource, bool allowNotifications)
        {
            Id = id;
            ReservedFor = new Interval(startTime, endTime);
            Owner = owner;
            ReservedResource = reservedResource;
            AllowNotifications = allowNotifications;
        }

        public long Id { get; }
        public Interval ReservedFor { get; }
        public DomainUser? Owner { get; }
        public DomainResource? ReservedResource { get; }
        public bool AllowNotifications { get; }
    }
}