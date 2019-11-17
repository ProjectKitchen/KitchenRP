using System.Text.Json;

namespace KitchenRP.DataAccess.Models
{
    public class Resource
    {
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

        public Resource(long id, string displayName, JsonDocument metaData, string description)
        {
            Id = id;
            DisplayName = displayName;
            MetaData = metaData;
            Description = description;
        }

        public Resource(long id, string displayName, string description)
        {
            Id = id;
            DisplayName = displayName;
            Description = description;
        }

        public long Id { get; private set; }
        public string DisplayName { get; private set; }
        public JsonDocument MetaData { get; private set; }
        public string Description { get; private set; }
        public ResourceType ResourceType { get; private set; }
    }
}