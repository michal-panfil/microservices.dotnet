using WarehouseService.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using WarehouseService.Core.Interfaces;
using WarehouseService.Infrastructure.Serices;

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