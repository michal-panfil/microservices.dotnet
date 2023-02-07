using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
        private readonly HttpClient httpClient;

        public NewOrderManager(IMessageBusSender<UpdateStatusMessage> meassageSender,
            IShipmentDataManager shipmentDataManager, IHttpClientFactory httpClientFactory)
        {
            this.meassageSender = meassageSender;
            this.shipmentDataManager = shipmentDataManager;
            this.httpClient = httpClientFactory.CreateClient();
        }


        public void ProcessNewOrder(OrderDto order)
        {
            // do some processing
            // send message to message bus
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
            this.httpClient.GetAsync($"http://warehousapi/api/ShipmentInfo/{order.Id}");

        }
    }
}
