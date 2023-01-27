using System;
using System.Collections.Generic;
using System.Linq;
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

        public NewOrderManager(IMessageBusSender<UpdateStatusMessage> meassageSender, IShipmentDataManager shipmentDataManager)
        {
            this.meassageSender = meassageSender;
            this.shipmentDataManager = shipmentDataManager;
        }

        public void ProcessNewOrder(OrderDto order)
        {
            // do some processing
            // send message to message bus
            Task.Delay(1000);
            this.shipmentDataManager.AddShipment(new Shipment
            {
                OrderId = order.Id,
                Address = order.ClientAddress,
                ReciverName = order.ClientName,
                KmToTarget = 600
            });;
            meassageSender.SendRabbitMqMessage(new UpdateStatusMessage()
            {
                OrderId = order.Id,
                Status = OrderStatus.InProcess
            }); ;
        }
    }
}
