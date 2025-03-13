using Newtonsoft.Json;
using System;

namespace WebApi.Models
{
    public class ProductInfoDTO
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public decimal Price { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DeleteDate { get; set; }
    }
}