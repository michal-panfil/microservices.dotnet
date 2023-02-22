using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WarehouseService.Core.Interfaces;
using WarehouseService.Core.Models;
using WarehouseService.Infrastructure.Data;
using WarehouseService.Infrastructure.Serices;

namespace WarehouseService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentInfo : ControllerBase
    {
        private readonly ILogger<ShipmentInfo> logger;
        private readonly IWarehouseRepository<Core.Models.Shipment> shipmenrRepo;

        public ShipmentInfo(
            IShipmentClient shipmentClient, ILogger<ShipmentInfo> logger,
            IWarehouseRepository<Core.Models.Shipment> shipmenrRepo)
        {
            ShipmentClient = shipmentClient;
            this.logger = logger;
            this.shipmenrRepo = shipmenrRepo;
        }

        public IShipmentClient ShipmentClient { get; }

        [HttpPost("{id:int}")]
        public async Task Post(int id)
        {
            var shipment = await this.shipmenrRepo.GetAsync(id);
            if (shipment == null)
            {
                logger.LogWarning($"Shipment with id {id} NOT found");
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }
            _ = ShipmentClient.GetShipmentInfo(shipment);

        }
    }
}
