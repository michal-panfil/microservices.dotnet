using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseService.Core.Models;

namespace WarehouseService.Core.Interfaces
{
    public interface IShipmentDataManager
    {
        void AddShipment(Shipment shipment);
    }
}
