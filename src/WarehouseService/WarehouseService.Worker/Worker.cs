using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using WarehouseService.Core.Models;
using WarehouseService.Core.Services;

namespace WarehouseService.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration configuration;
        private readonly NewOrderManager newOrderManager;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, NewOrderManager newOrderManager)
        {
            _logger = logger;
            this.configuration = configuration;
            this.newOrderManager = newOrderManager;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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
            channel.QueueDeclare(queue: configuration["RabbitMq:Queues:OrderDto"],
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
                var updateMsg = System.Text.Json.JsonSerializer.Deserialize<OrderDto>(message, options);
                Console.WriteLine(updateMsg.ToString());
                this.newOrderManager.ProcessNewOrder(updateMsg);

            };
            channel.BasicConsume(queue: configuration["RabbitMq:Queues:OrderDto"],
                                 autoAck: true,
                                 consumer: consumer);

        }
    }
}