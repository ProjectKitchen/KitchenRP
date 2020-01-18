using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KitchenRP.Domain.Models
{
    
    
    public class DomainReservationStatus
    {
        public const string Pending = "PENDING";
        public const string NeedsApproval = "NEEDS_APPROVAL";
        public const string Denied = "DENIED";
        public const string Approved = "APPROVED";

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
                    {Pending, new DomainReservationStatus("PENDING", "Reservation pending ...")},
                    {NeedsApproval, new DomainReservationStatus("NEEDS_APPROVAL", "Reservation needs approval ...")},
                    {Denied, new DomainReservationStatus("DENIED", "Reservation was denied!")},
                    {Approved, new DomainReservationStatus("APPROVED", "Reservation was approved!")},
                }
            );
    }
}