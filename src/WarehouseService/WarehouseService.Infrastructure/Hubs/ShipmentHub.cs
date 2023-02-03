using Microsoft.AspNetCore.SignalR;
using WarehouseService.Core.Models;

namespace WarehouseService.Infrastructure.Hubs
{
    public class ShipmentHub : Hub
    {
        public async Task NewMessage(ShipmentDto shipment, string message) =>
         await Clients.All.SendAsync("messageReceived", shipment, message);
    }

}
