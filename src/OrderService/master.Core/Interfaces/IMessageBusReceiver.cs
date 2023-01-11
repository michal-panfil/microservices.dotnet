using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Core.Interfaces
{
    public interface IMessageBusReceiver
    {
        Task ProcessMessages(CancellationToken stoppingToken);
    }
}
