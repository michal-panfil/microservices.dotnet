using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersService.Core.Interfaces;
using OrdersService.Core.Models;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace OrdersService.Infrastructure.Services
{
    public class MessageBusReceiver : IMessageBusReceiver
    {
        private readonly IConfiguration configuration;

        public MessageBusReceiver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task ProcessMessages(CancellationToken stoppingToken)
        {
            Console.WriteLine("starting message receiver service");
            var factory = new ConnectionFactory()
            {
                HostName = configuration["RabbitMq:HostName"],
                UserName = configuration["RabbitMq:UserName"],
                Password = configuration["RabbitMq:Password"],
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: configuration["RabbitMq:StatusQueueName"],
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
                var options = new System.Text.Json.JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }

                };
                var updateMsg = System.Text.Json.JsonSerializer.Deserialize<UpdateOrderStatusMessage>(message, options);
                Console.WriteLine(updateMsg.ToString());
            };
            channel.BasicConsume(queue: configuration["RabbitMq:StatusQueueName"],
                                 autoAck: true,
                                 consumer: consumer);

            await Task.CompletedTask;

        }
    }
}
