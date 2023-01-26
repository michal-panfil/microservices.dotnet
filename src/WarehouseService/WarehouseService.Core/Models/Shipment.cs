using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseService.Core.Models
{
    public class Shipment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Address { get; set; } = string.Empty;
        public string ReciverName { get; set; } = string.Empty;
        public int KmToTarget { get; set; }
    }
}
