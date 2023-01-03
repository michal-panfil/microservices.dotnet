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

namespace master.Infrastructure.HostedServices
{
    public class MessageReceiverService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ProcessMessages(stoppingToken);
        }
        private async Task ProcessMessages(CancellationToken stoppingToken)
        {
            Console.WriteLine("starting message receiver service");
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "password"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "OrderUpdateStatus",
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
                    var updateMsg = System.Text.Json.JsonSerializer.Deserialize<UpdateOrderStatusMessage>(message, options );
                    Console.WriteLine(updateMsg.ToString());
                };
                channel.BasicConsume(queue: "OrderUpdateStatus",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
                await Task.CompletedTask;

            }
        }

    }
}
