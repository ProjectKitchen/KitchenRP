using System.ComponentModel.DataAnnotations;

namespace KitchenRP.Web.Models
{
    public class RefreshAccessRequest
    {
        [Required] public string? RefreshToken { get; set; }
    }
}