using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KitchenRP.Domain.Models.Messages
{
    public class NewResourceRequest
    {
        public string DisplayName { get; set; }
        [JsonConverter(typeof(NewResourceRequestJsonConverter))]
        public JsonDocument MetaData { get; set; }
        public string Description { get; set; }
        public string ResourceTypeName { get; set; }
    }

    public class NewResourceRequestJsonConverter : JsonConverter<JsonDocument>
    {
        public override JsonDocument Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonDocument.ParseValue(ref reader);
        }

        public override void Write(Utf8JsonWriter writer, JsonDocument value, JsonSerializerOptions options)
        {
            value.WriteTo(writer);
        }
    }
}