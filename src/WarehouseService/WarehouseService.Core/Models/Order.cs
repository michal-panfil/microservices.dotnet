﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseService.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public IList<OrderItem> Items { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}