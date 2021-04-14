﻿using System;
using System.Text.Json.Serialization;
using WebAPI.Data.Common;

namespace WebAPI.Entities
{
    public class OrderProduct : BaseEntity
    {
        public Guid OrderId { get; set; }

        [JsonIgnore]
        public virtual Order Order { get; set; }

        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
