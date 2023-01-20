using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersService.Core.Models;
using OrdersService.Core.Interfaces;

namespace OrdersService.Infrastructure.HostedServices
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
            messageBusReceiver.ProcessMessages(stoppingToken);
            await Task.CompletedTask;
        }

    }
}
