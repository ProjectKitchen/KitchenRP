using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KitchenRP.Web.Models
{
    public class AddResourceRequest
    {
        [Required] public string? DisplayName { get; set; }

        [JsonConverter(typeof(NewResourceRequestJsonConverter))]
        public JsonDocument? MetaData { get; set; }

        [Required] public string? Description { get; set; }

        [Required] public string? ResourceTypeName { get; set; }
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