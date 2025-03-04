﻿using Microsoft.Extensions.Hosting;
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
using OrdersService.Infrastructure.Models;

namespace OrdersService.Infrastructure.HostedServices
{
    public class MessageReceiverService : BackgroundService
    {
        private readonly IMessageBusReceiver<UpdateStatusMessage> messageBusReceiver;

        public MessageReceiverService(IMessageBusReceiver<UpdateStatusMessage> messageBusReceiver)
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
