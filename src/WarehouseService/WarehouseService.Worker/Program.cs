using WarehouseService.Infrastructure;

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
                    services.AddHostedService<Worker>();
                    services.AddDatabase(config);
                    services.AddDomainServices(config);
                })
                .Build();
            host.MigrateDatabase();
            host.Run();
        }
    }
}