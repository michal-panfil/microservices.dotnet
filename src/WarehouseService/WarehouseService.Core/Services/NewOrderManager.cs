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

        public NewOrderManager(IMessageBusSender<UpdateStatusMessage> meassageSender)
        {
            this.meassageSender = meassageSender;
        }

        public void ProcessNewOrder(OrderDto order)
        {
            // do some processing
            // send message to message bus
            meassageSender.SendRabbitMqMessage(new UpdateStatusMessage()
            {
                OrderId = order.Id,
                Status = OrderStatus.InProcess
            }); ;
        }
    }
}
