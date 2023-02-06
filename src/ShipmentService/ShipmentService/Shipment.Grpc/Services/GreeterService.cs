using Grpc.Core;
using Shipment.Grpc;

namespace Shipment.Grpc.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;

        private string[] Cities = new string[] { "Warszawa", "Poznañ", "Berlin", "Pary¿", "Lyon", "Marsylia", "Montpellier", "Tuluza" };
    public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
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