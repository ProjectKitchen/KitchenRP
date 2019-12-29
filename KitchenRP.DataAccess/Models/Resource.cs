using System.Text.Json;

namespace KitchenRP.DataAccess.Models
{
    public class Resource
    {
        public Resource()
        {
        }

        public Resource(
            string displayName,
            JsonDocument metaData,
            string description,
            ResourceType resourceType)
        {
            DisplayName = displayName;
            MetaData = metaData;
            Description = description;
            ResourceType = resourceType;
        }

        public Resource(
            long id,
            string displayName,
            JsonDocument metaData,
            string description,
            ResourceType resourceType)
        {
            Id = id;
            DisplayName = displayName;
            MetaData = metaData;
            Description = description;
            ResourceType = resourceType;
        }

        public long Id { get; set; }
        public string DisplayName { get; set; }
        public JsonDocument MetaData { get; set; }
        public string Description { get; set; }
        public ResourceType ResourceType { get; set; }
        public long? ResourceTypeId { get; set; }
    }
}