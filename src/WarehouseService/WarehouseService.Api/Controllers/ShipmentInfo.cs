using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseService.Infrastructure.Data;
using WarehouseService.Infrastructure.Serices;

namespace WarehouseService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentInfo : ControllerBase
    {
        private readonly WarehouseContext context;

        public ShipmentInfo(WarehouseContext context, ShipmentClient shipmentClient)
        {
            this.context = context;
            ShipmentClient = shipmentClient;
        }

        public ShipmentClient ShipmentClient { get; }

        [HttpGet("{id:int}")]
        public async Task Get(int id)
        {
            //var shipment = this.context.Shipments.FirstOrDefault(s => s.Id == id);
            //if (shipment == null)
            //{
            //    this.Response.StatusCode = StatusCodes.Status404NotFound;
            //    return;
            //}
            // var shipment = new Core.Models.Shipment { OrderId = id, KmToTarget = shipment.KmToTarget }
            var shipment = new Core.Models.Shipment { OrderId = 101, KmToTarget = 600 };

            await this.ShipmentClient.GetShipmentInfo(shipment);

        }
    }
}
