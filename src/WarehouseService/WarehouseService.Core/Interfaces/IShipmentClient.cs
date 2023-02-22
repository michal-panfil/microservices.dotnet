namespace WarehouseService.Core.Interfaces
{
    public interface IShipmentClient
    {
        Task GetShipmentInfo(Models.Shipment shipment);
    }
}
