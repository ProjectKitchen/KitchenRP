using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using NodaTime;
using NodaTime.Text;

namespace KitchenRP.Web.Models
{
    public class AddReservationRequest
    {
        [Required]
        [JsonConverter(typeof(AddReservationRequestJsonConverter))]
        public Instant? StartTime { get; set; }

        [Required]
        [JsonConverter(typeof(AddReservationRequestJsonConverter))]
        public Instant? EndTime { get; set; }

        [Required] public long? UserId { get; set; }

        [Required] public long? ResourceId { get; set; }

        [Required] public bool? AllowNotifications { get; set; }
    }
    public class AddReservationRequestJsonConverter : JsonConverter<Instant?>
    {
        public override Instant? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var pattern = InstantPattern.ExtendedIso;
            var result = pattern.Parse(reader.GetString());
            return result.GetValueOrThrow();
        }

        public override void Write(Utf8JsonWriter writer, Instant? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
