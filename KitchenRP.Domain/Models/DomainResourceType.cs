namespace KitchenRP.Domain.Models
{
    public class DomainResourceType
    {
        public DomainResourceType(string type, string displayName)
        {
            Type = type;
            DisplayName = displayName;
        }

        public string Type { get; }
        public string DisplayName { get; }
    }
}