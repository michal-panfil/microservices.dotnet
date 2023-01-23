using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WarehouseService.Core.Interfaces;

namespace WarehouseService.Infrastructure.Serices
{
    public class MessageBusSender<T> : IMessageBusSender<T>
    {
        private readonly IConfiguration configuration;

        public MessageBusSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void SendRabbitMqMessage(T order)
        {
            var factory = new ConnectionFactory()
            {
                HostName = configuration["RabbitMq:HostName"],
                UserName = configuration["RabbitMq:UserName"],
                Password = configuration["RabbitMq:Password"],
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(configuration[$"RabbitMq:Queues:{typeof(T).Name}"],
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                var message = JsonSerializer.Serialize(order);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: configuration[$"RabbitMq:Queues:{typeof(T).Name}"],
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", order);
            }
        }
    }
}
