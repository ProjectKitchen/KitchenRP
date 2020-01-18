using System.ComponentModel.DataAnnotations;

namespace KitchenRP.Web.Models
{
    public class AddResourceTypeRequest
    {
        [Required] public string? Type { get; set; }

        [Required] public string? DisplayName { get; set; }
    }
}