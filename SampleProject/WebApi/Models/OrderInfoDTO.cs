using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class OrderInfoDTO
    {
        public Guid Id { get; set; }

        public IEnumerable<OrderItemInfoDTO> OrderItems { get; set; }

        public decimal Total { get; set; }

        public DateTime CreateDate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DeleteDate { get; set; }
    }
}