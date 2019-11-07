using System.Text.Json;

namespace KitchenRP.Domain.Models
{
    public class DomainResource
    {
        public DomainResource(long id, string displayName, JsonDocument metaData, string description,
            DomainResourceType? resourceType)
        {
            Id = id;
            DisplayName = displayName;
            MetaData = metaData;
            Description = description;
            ResourceType = resourceType;
        }

        public long Id { get; }
        public string DisplayName { get; }
        public JsonDocument MetaData { get; }
        public string Description { get; }
        public DomainResourceType? ResourceType { get; }
    }
}