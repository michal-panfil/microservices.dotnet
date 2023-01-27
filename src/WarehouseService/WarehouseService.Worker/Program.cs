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
                    services.AddDatabase(config);
                    services.AddDomainServices(config);
                    services.AddHostedService<Worker>();
                })
                .Build();
            //host.MigrateDatabase();
            host.Run();
        }
    }
}