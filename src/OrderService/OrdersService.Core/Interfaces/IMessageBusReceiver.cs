using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Core.Interfaces
{
    public interface IMessageBusReceiver<T>
    {
        void ProcessMessages(CancellationToken stoppingToken);
    }
}
