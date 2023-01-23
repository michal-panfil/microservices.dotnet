using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseService.Core.Enums;

namespace WarehouseService.Core.Models.Messages
{
    public class UpdateStatusMessage
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
