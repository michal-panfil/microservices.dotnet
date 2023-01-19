using OrdersService.Core.Models;
using OrdersService.Core.Models.Dtos;
using OrdersService.Core.Models.Entities;
using OrdersService.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OrdersService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public OrderController(OrderContext dbContext)
        {
            DbContext = dbContext;
        }

        public OrderContext DbContext { get; }

        [HttpGet]
        public List<Order> Get()
        {
            return this.DbContext.Orders.Include(x => x.Product).ToList();
        }

        [HttpPost]
        public IActionResult Post(OrderDto order)
        {

            var newOrder = new Order
            {
                ClientName = order.ClientName,
                ClientAddress = order.ClientAddress,
                Quantity = order.Quantity,
                ProductId = order.ProductId
            };
            this.DbContext.Orders.Add(newOrder);
            // sent message
            return Ok();
        }
    }
}