using System.ComponentModel.DataAnnotations;

namespace KitchenRP.Web.Models
{
    public class NewResourceTypeRequest
    {
        [Required]
        public string? Type { get; set; }

        [Required]
        public string? DisplayName { get; set; }
    }
}