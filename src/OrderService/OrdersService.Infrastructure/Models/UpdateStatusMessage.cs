using OrdersService.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Infrastructure.Models
{
    public class UpdateStatusMessage
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
