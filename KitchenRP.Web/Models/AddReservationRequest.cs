using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace KitchenRP.Web.Models
{
    public class AddReservationRequest
    {
        [Required]
        public Instant? StartTime { get; set; }

        [Required]
        public Instant? EndTime { get; set; }

        [Required]
        public long? UserId { get; set; }

        [Required]
        public long? ResourceId { get; set; }

        [Required]
        public bool? AllowNotifications { get; set; }
    }
}
