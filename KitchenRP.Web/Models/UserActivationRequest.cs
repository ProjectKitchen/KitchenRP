using System.ComponentModel.DataAnnotations;

namespace KitchenRP.Web.Models
{
    public class UserActivationRequest
    {
        [Required]
        public string? Uid { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
    }
}