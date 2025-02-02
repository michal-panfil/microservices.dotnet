using WarehouseService.Infrastructure;
using WarehouseService.Core.Interfaces;
using WarehouseService.Infrastructure.Serices;
using WarehouseService.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace WarehouseService.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    var config = services.BuildServiceProvider().GetService<IConfiguration>();
                    services.Configure<RabitMqSettings>(config.GetSection("RabbitMq"));

                    services.AddScoped<IWarehouseApiClient, WarehouseApiClient>();

                    services.AddDatabase(config);
                    services.MigrateDatabase();
                    services.AddHttpClient();

                    services.AddDomainServices(config);
                    services.AddHostedService<Worker>();
                })
                .Build();
            host.Run();
        }
    }
}