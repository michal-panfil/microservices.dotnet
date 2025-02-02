using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using WarehouseService.Core.Interfaces;
using WarehouseService.Infrastructure.Settings;

namespace WarehouseService.Infrastructure.Serices
{
    public class MessageBusSender<T> : IMessageBusSender<T>
    {
        private readonly RabitMqSettings settings;

        public MessageBusSender(IOptions<RabitMqSettings> settings)
        {
            this.settings = settings.Value;
        }
        public void SendRabbitMqMessage(T order)
        {
            var factory = new ConnectionFactory()
            {
                HostName = this.settings.HostName,
                UserName = this.settings.UserName,
                Password = this.settings.Password,
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(this.settings.Queues[typeof(T).Name],
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                var message = JsonSerializer.Serialize(order);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: this.settings.Queues[typeof(T).Name],
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", order);
            }
        }
    }
}
