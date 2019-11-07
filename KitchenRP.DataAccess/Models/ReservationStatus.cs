using System.ComponentModel.DataAnnotations.Schema;

namespace KitchenRP.DataAccess.Models
{
    public class ReservationStatus
    {
        public ReservationStatus(long id, string status, string displayName)
        {
            Id = id;
            Status = status;
            DisplayName = displayName;
        }

        public long Id { get; private set; }

        public string Status { get; private set; }

        public string DisplayName { get; private set; }
    }
}