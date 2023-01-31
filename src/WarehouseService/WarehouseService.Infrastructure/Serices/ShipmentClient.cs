using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseService.Core.Models;

namespace WarehouseService.Infrastructure.Serices
{
    public class ShipmentClient
    {
        public async Task GetShipmentInfo(WarehouseService.Core.Models.Shipment shipment)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5132");
            var client = new Shipment.Grpc.Greeter.GreeterClient(channel);

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            using var streamingCall = client.GetKMToTarget(new Shipment.Grpc.ShipmentRequest { ShipmentId = shipment.OrderId , InitialKM = 600}, cancellationToken: cts.Token);

            try
            {
                await foreach (var shipmentReply in streamingCall.ResponseStream.ReadAllAsync(cancellationToken: cts.Token))
                {
                    Console.WriteLine($"{shipmentReply.ShipmentId} | {shipmentReply.RemainingKm} | {shipmentReply.CurrentLocation}");
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                Console.WriteLine("Stream cancelled.");
            }
        }
    }
}
