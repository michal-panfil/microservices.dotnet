using Grpc.Core;

namespace Shipment.Grpc.Services
{
    public class ShipmentMonitorService : ShipmentMonitor.ShipmentMonitorBase
    {
        private readonly ILogger<ShipmentMonitorService> _logger;

        private string[] Cities = new string[] { "Warszawa", "Poznañ", "Berlin", "Pary¿", "Lyon", "Marsylia", "Montpellier", "Tuluza" };
        public ShipmentMonitorService(ILogger<ShipmentMonitorService> logger)
        {
            _logger = logger;
        }

        public override async Task GetKMToTarget(ShipmentRequest request, IServerStreamWriter<ShipmentReply> responseStream, ServerCallContext context)
        {
            var currentKM = request.InitialKM;
            while (!context.CancellationToken.IsCancellationRequested && currentKM > 0)
            {
                Thread.Sleep(300);
                currentKM -= 10;

                await responseStream.WriteAsync(new ShipmentReply
                {
                    ShipmentId = request.ShipmentId,
                    RemainingKm = currentKM,
                    CurrentLocation = Cities[currentKM / 100]
                });

            }

        }
    }
}