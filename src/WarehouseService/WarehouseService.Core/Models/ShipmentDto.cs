using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseService.Core.Models
{
    public class ShipmentDto
    {
        public int ShipmentId { get; set; }
        public int RemainingKm { get; set; }
        public string CurrentLocation { get; set; }
    }
}
