using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseService.Core.Interfaces
{
    public interface IMessageBusSender<T>
    {
        void SendRabbitMqMessage(T order);
    }
}
