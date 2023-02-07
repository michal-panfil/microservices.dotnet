using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WarehouseService.Core.Interfaces;
using WarehouseService.Infrastructure.Data;
using WarehouseService.Infrastructure.Serices;

namespace WarehouseService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentInfo : ControllerBase
    {
        private readonly WarehouseContext context;
        private readonly ILogger<ShipmentInfo> logger;

        public ShipmentInfo(WarehouseContext context, IShipmentClient shipmentClient, ILogger<ShipmentInfo> logger)
        {
            this.context = context;
            ShipmentClient = shipmentClient;
            this.logger = logger;
        }

        public IShipmentClient ShipmentClient { get; }

        [HttpGet("{id:int}")]
        public async Task Get(int id)
        {
            var shipment = this.context.Shipments.FirstOrDefault(s => s.OrderId == id);
            if (shipment == null)
            {
                logger.LogWarning($"Shipment with id {id} NOT found");
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }
            logger.LogInformation($"Shipment with id {id} found");
            _ = ShipmentClient.GetShipmentInfo(shipment);
            await Task.CompletedTask;

        }
    }
}
