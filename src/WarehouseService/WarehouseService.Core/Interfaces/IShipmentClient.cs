using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseService.Core.Interfaces
{
    public interface IShipmentClient
    {
        Task GetShipmentInfo(WarehouseService.Core.Models.Shipment shipment);
    }
}
