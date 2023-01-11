using master.Core.Models;
using master.Core.Models.Dtos;
using master.Core.Models.Entities;
using master.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace master.Api.Controllers
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
            return this.DbContext.Orders.Include(x => x.Items).ThenInclude(x => x.Product).Include(x => x.Customer).ToList();
        }

        [HttpPost]
        public IActionResult Post(OrderDto order)
        {
            var orderEntity = new Order
            {
                CustomerId = order.CustomerId,
                Items = order.Items.Select(x => new OrderItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity
                }).ToList()
            };
            this.DbContext.Orders.Add(orderEntity);
            this.DbContext.SaveChanges();
            return Ok();
        }
    }
}