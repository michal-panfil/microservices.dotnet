using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.SignalR;
using WarehouseService.Core.Interfaces;
using WarehouseService.Core.Models;
using WarehouseService.Infrastructure.Hubs;

namespace WarehouseService.Infrastructure.Serices
{
    public class ShipmentClient: IShipmentClient
    {
        private readonly IHubContext<ShipmentHub> hub;

        public ShipmentClient(IHubContext<ShipmentHub> hub )
        {
            this.hub = hub;
        }
        public async Task GetShipmentInfo(WarehouseService.Core.Models.Shipment shipment)
        {
            using var channel = GrpcChannel.ForAddress("http://shipmentgrpc:80");

            var client = new ShipmentGrpc.Grpc.ShipmentMonitor.ShipmentMonitorClient(channel);

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(100));
            using var streamingCall = client.GetKMToTarget(new ShipmentGrpc.Grpc.ShipmentRequest { ShipmentId = shipment.OrderId, InitialKM = 600 }, cancellationToken: cts.Token);

            try
            {
                await foreach (var shipmentReply in streamingCall.ResponseStream.ReadAllAsync(cancellationToken: cts.Token))
                {
                    Console.WriteLine($"{shipmentReply.ShipmentId} | {shipmentReply.RemainingKm} | {shipmentReply.CurrentLocation}");
                    await this.hub.Clients.All.SendAsync("newshipmentlocation", new ShipmentDto { ShipmentId = shipmentReply.ShipmentId, RemainingKm = shipmentReply.RemainingKm, CurrentLocation = shipmentReply.CurrentLocation }); ;
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                Console.WriteLine("Stream cancelled.");
            }
        }
    }
}
