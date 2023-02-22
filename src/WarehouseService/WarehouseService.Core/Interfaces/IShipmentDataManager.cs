using WarehouseService.Core.Models;

namespace WarehouseService.Core.Interfaces
{
    public interface IShipmentDataManager
    {
        void AddShipment(Shipment shipment);
    }
}
