using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseService.Infrastructure.Serices;

namespace WarehouseService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentInfo : ControllerBase
    {
        [HttpGet]
        public async Task Get()
        {
            var service = new ShipmentClient();
            await service.GetShipmentInfo(new Core.Models.Shipment { OrderId = 1, KmToTarget= 600 });

        }
    }
}
