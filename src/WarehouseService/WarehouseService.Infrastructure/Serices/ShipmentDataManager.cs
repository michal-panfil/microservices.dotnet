using Microsoft.Extensions.DependencyInjection;
using WarehouseService.Core.Interfaces;
using WarehouseService.Infrastructure.Data;

namespace WarehouseService.Infrastructure.Serices;

public class ShipmentDataManager : IShipmentDataManager
{
    private readonly IServiceScopeFactory serviceScopeFactory;

    public ShipmentDataManager(IServiceScopeFactory serviceScopeFactory)
    {
        this.serviceScopeFactory = serviceScopeFactory;
    }
    public void AddShipment(Core.Models.Shipment shipment)
    {
        using var scope = this.serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<WarehouseContext>();
        context.Shipments.Add(shipment);
        context.SaveChanges();
    }
}
