using KitchenRP.DataAccess.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KitchenRP.Web.Models
{
    public class UpdateResourceRequest : AddResourceRequest
    {
        [Required] public long? Id { get; set; }

        public string? DisplayName { get; set; }

        [JsonConverter(typeof(NewResourceRequestJsonConverter))]
        public JsonDocument? MetaData { get; set; }

        public string? Description { get; set; }

        public string? ResourceTypeName { get; set; }
    }
}