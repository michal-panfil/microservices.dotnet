using WarehouseService.Core.Enums;
using WarehouseService.Core.Interfaces;
using WarehouseService.Core.Models;
using WarehouseService.Core.Models.Messages;

namespace WarehouseService.Core.Services
{
    public class NewOrderManager
    {
        private readonly IMessageBusSender<UpdateStatusMessage> meassageSender;
        private readonly IShipmentDataManager shipmentDataManager;
        private readonly IWarehouseApiClient warehouseApiClient;

        public NewOrderManager(
            IMessageBusSender<UpdateStatusMessage> meassageSender,
            IShipmentDataManager shipmentDataManager,
            
            IWarehouseApiClient warehouseApiClient)
        { 
            this.meassageSender = meassageSender;
            this.shipmentDataManager = shipmentDataManager;
            this.warehouseApiClient = warehouseApiClient;
            
            
        }

        
    public async void ProcessNewOrder(OrderDto order)
        {
            Task.Delay(1000);
            var shipment = new Shipment
            {
                OrderId = order.Id,
                Address = order.ClientAddress,
                ReciverName = order.ClientName,
                KmToTarget = 600
            };
            this.shipmentDataManager.AddShipment(shipment);
            this.meassageSender.SendRabbitMqMessage(new UpdateStatusMessage()
            {
                OrderId = order.Id,
                Status = OrderStatus.InProcess
            });

           await this.warehouseApiClient.StartShipment(shipment);


        }
    }
}
