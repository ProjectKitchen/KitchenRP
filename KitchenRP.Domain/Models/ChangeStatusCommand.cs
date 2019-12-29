using KitchenRP.DataAccess.Models;

namespace KitchenRP.Domain.Models
{
    public class ChangeStatusCommand
    {
        public string Reason { get; set; }
        public DomainReservation Reservation { get; set; }
        public User ChangedBy { get; set; }
        public DomainReservationStatus ChangeTo { get; set; }
    }
}