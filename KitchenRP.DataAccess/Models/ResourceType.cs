namespace KitchenRP.DataAccess.Models
{
    public class ResourceType
    {
        public ResourceType(long id, string type, string displayName)
        {
            Id = id;
            Type = type;
            DisplayName = displayName;
        }

        public ResourceType(string type, string displayName)
        {
            Type = type;
            DisplayName = displayName;
        }
        
        public long? Id { get; private set; }
        public string Type { get; private set; }
        public string DisplayName { get; private set; }
    }
}