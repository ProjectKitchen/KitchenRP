using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KitchenRP.Domain.Models
{
    public class DomainReservationStatus
    {
        private DomainReservationStatus(string status, string displayName)
        {
            Status = status;
            DisplayName = displayName;
        }

        public string Status { get; }
        public string DisplayName { get; }


        public static readonly ReadOnlyDictionary<string, DomainReservationStatus> ReservationStatuses
            = new ReadOnlyDictionary<string, DomainReservationStatus>(
                new Dictionary<string, DomainReservationStatus>
                {
                    {"PENDING", new DomainReservationStatus("PENDING", "Reservation pending ...")},
                    {"NEEDS_APPROVAL", new DomainReservationStatus("NEEDS_APPROVAL", "Reservation needs approval ...")},
                    {"DENIED", new DomainReservationStatus("DENIED", "Reservation was denied!")},
                    {"APPROVED", new DomainReservationStatus("APPROVED", "Reservation was approved!")},
                }
            );
    }
}