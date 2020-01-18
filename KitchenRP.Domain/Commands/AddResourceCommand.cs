using System.Text.Json;

namespace KitchenRP.Domain.Commands
{
    public class AddResourceCommand
    {
        public string DisplayName { get; set; }
        public JsonDocument MetaData { get; set; }
        public string Description { get; set; }
        public string ResourceTypeName { get; set; }
    }
}