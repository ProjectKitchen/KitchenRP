using KitchenRP.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace KitchenRP.Domain.Commands
{
    public class UpdateResourceCommand
    {
        public long id { get; set; }
        public string DisplayName { get; set; }
        public JsonDocument MetaData { get; set; }
        public string Description { get; set; }
        public string ResourceTypeName { get; set; }
    }
}
