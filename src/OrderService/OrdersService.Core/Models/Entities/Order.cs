using OrdersService.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Core.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string ClientAddress { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = new();

        public OrderStatus Status { get; set; }

        public int TimeToTarget { get; set; }
    }
    
}

