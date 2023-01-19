using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersService.Core.Models.Enums;

namespace OrdersService.Core.Models
{
    public class UpdateOrderStatusMessage
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }

        public DateTime Date { get; set; }

        public override string ToString() => $"OrderId: {OrderId}, Status: {Status}, Date: {Date}";
    }
}
