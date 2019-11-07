using System;

namespace KitchenRP.Web.Models
{
    public class ResponseError
    {
        public string? Title { get; set; }
        public int? Status { get; set; }
        public string? Detail { get; set; }
    }
}