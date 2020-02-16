using System.Collections.Generic;
using KitchenRP.DataAccess.Models;
using NodaTime;

namespace KitchenRP.Domain.Models
{
    public class DomainReservation
    {
        public DomainReservation(long id, Instant startTime, Instant endTime, DomainUser? owner,
            DomainResource? reservedResource, bool allowNotifications) //  HashSet<StatusChange>? statusChanges,
        {
            Id = id;
            ReservedFor = new Interval(startTime, endTime);
            Owner = owner;
            ReservedResource = reservedResource;
            // StatusChanges = statusChanges;
            AllowNotifications = allowNotifications;
        }

        public long Id { get; }
        public Interval ReservedFor { get; }
        public DomainUser? Owner { get; }
        public DomainResource? ReservedResource { get; }
        // public HashSet<StatusChange>? StatusChanges { get; } // TODO: use right class
        public bool AllowNotifications { get; }
    }
}