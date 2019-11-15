using System.ComponentModel.DataAnnotations;

namespace KitchenRP.Web.Models
{
    public class DestroyTokenRequest
    {
        [Required]
        public string? Token { get; set; }
    }
}
