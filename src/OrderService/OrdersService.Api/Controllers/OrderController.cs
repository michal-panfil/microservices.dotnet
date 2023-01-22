using OrdersService.Core.Models;
using OrdersService.Core.Models.Dtos;
using OrdersService.Core.Models.Entities;
using OrdersService.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersService.Infrastructure.Services;

namespace OrdersService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly MessageBusSender<OrderDto> messageSender;
        private readonly OrderContext dbContext;

        public OrderController(OrderContext dbContext, MessageBusSender<OrderDto> messageSender)
        {
            this.dbContext = dbContext;
            this.messageSender = messageSender;
        }


        [HttpGet]
        public List<Order> Get()
        {
            return this.dbContext.Orders.Include(x => x.Product).ToList();
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
            this.dbContext.Orders.Add(newOrder);
            this.dbContext.SaveChanges();
            // sent message
            this.messageSender.SendRabbitMqMessage(order);
            return Ok();
        }
    }
}