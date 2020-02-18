using System.ComponentModel.DataAnnotations;

namespace KitchenRP.Web.Models
{
    public class StatusChangeRequest
    {
        [Required] public long Id { get; set; }
        [Required] public long UserId { get; set; }
    }
}