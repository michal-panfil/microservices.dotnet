using WarehouseService.Core.Models;

namespace WarehouseService.Core.Interfaces
{
    public interface IWarehouseApiClient
    {
        Task StartShipment(Shipment shipment);
    }
}
