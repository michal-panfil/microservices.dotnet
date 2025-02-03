using Microsoft.AspNetCore.Mvc;
using WarehouseService.Core.Interfaces;

namespace WarehouseService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentInfo : ControllerBase
    {
        private readonly ILogger<ShipmentInfo> logger;
        private readonly IWarehouseRepository<Core.Models.Shipment> shipmenrRepo;
        private readonly IShipmentClient shipmentClient;

        public ShipmentInfo(
            IShipmentClient shipmentClient, 
            ILogger<ShipmentInfo> logger,
            IWarehouseRepository<Core.Models.Shipment> shipmenrRepo)
        {
            this.shipmentClient = shipmentClient;
            this.logger = logger;
            this.shipmenrRepo = shipmenrRepo;
        }

        [HttpPost("{id:int}")]
        public async Task Post(int id)
        {
            var shipment = await this.shipmenrRepo.GetAsync(id);
            if (shipment is null)
            {
                logger.LogWarning($"Shipment with id {id} NOT found");
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }
            await this.shipmentClient.GetShipmentInfo(shipment);

        }
    }
}
