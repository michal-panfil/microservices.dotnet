using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using OrdersService.Core.Interfaces;
using OrdersService.Core.Models;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

namespace OrdersService.Infrastructure.Services
{
    public class MessageBusReceiver<T> : IMessageBusReceiver<T>
    {
        private readonly IConfiguration configuration;

        public MessageBusReceiver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ProcessMessages(CancellationToken stoppingToken)
        {
            Console.WriteLine("starting message receiver service");
            var queueName = configuration[$"RabbitMq:Queues:{typeof(T).Name}"];
            var factory = new ConnectionFactory()
            {
                HostName = configuration["RabbitMq:HostName"],
                UserName = configuration["RabbitMq:UserName"],
                Password = configuration["RabbitMq:Password"],
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName,
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
                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }

                };
                var updateMsg = JsonSerializer.Deserialize<UpdateOrderStatusMessage>(message, options);
                Console.WriteLine(updateMsg.ToString());
            };
            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);


        }
    }
}
