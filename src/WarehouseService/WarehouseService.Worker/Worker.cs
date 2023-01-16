using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using WarehouseService.Core.Models;

namespace WarehouseService.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("starting message receiver service");
            var factory = new ConnectionFactory()
            {
                HostName = "rabbitmq",
                UserName = "user",
                Password = "password"
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "NewOrderMessage",
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
                var updateMsg = System.Text.Json.JsonSerializer.Deserialize<Order>(message, options);
                Console.WriteLine(updateMsg.ToString());
            };
            channel.BasicConsume(queue: "NewOrderMessage",
                                 autoAck: true,
                                 consumer: consumer);

        }
    }
}