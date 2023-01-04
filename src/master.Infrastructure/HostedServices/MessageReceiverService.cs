using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Core.Models;
using master.Core.Interfaces;

namespace master.Infrastructure.HostedServices
{
    public class MessageReceiverService : BackgroundService
    {
        private readonly IMessageBusReceiver messageBusReceiver;

        public MessageReceiverService(IMessageBusReceiver messageBusReceiver)
        {
            this.messageBusReceiver = messageBusReceiver;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await messageBusReceiver.ProcessMessages(stoppingToken);
        }

    }
}
