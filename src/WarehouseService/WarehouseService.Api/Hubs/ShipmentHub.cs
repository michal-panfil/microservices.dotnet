using Microsoft.AspNetCore.SignalR;

namespace WarehouseService.Api.Hubs
{
    public class ShipmentHub : Hub
    {
        public async Task NewMessage(long username, string message) =>
         await Clients.All.SendAsync("messageReceived", username, message);
    }

}
