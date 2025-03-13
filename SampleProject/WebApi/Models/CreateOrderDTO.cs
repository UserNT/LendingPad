using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class CreateOrderDTO
    {
        public Dictionary<Guid, int> Items { get; set; }
    }
}