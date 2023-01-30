using Grpc.Core;
using Shipment.Grpc;

namespace Shipment.Grpc.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
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
        public override Task<ShipmentReply> GetKMToTarget(ShipmentRequest request, ServerCallContext context)
        {
            Task.Delay(1000).Wait();
            return Task.FromResult(new ShipmentReply()
            {
                ShipmentId = request.ShipmentId,
                RemainingKm = request.InitialKM - 10 >= 0 ? request.InitialKM - 10 : 0,
                CurrentLocation = "Poland"
            });
                
        }
        
    }
}