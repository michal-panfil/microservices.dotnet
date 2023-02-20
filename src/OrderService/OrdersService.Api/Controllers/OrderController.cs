using OrdersService.Core.Models;
using OrdersService.Core.Models.Dtos;
using OrdersService.Core.Models.Entities;
using OrdersService.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersService.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;

namespace OrdersService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly MessageBusSender<OrderDto> messageSender;
        private readonly OrderRepository orderRepo;

        public OrderController(OrderRepository orderRepo, MessageBusSender<OrderDto> messageSender)
        {
            this.orderRepo = orderRepo;
            this.messageSender = messageSender;
        }

        
        [HttpGet]
        public List<OrderDto> Get()
        {
            return this.orderRepo.GetOrders(x => true).Include(x => x.Product).Select(p => new OrderDto
            {
                Id = p.Id,
                ClientName = p.ClientName,
                ClientAddress = p.ClientAddress,
                Quantity = p.Quantity,
                ProductId = p.ProductId,
                ProductName = string.IsNullOrEmpty(p.Product.Name) ? "undefined" : p.Product.Name,
                Status = p.Status.ToString()

            }).ToList();
        }

        [HttpPost]
        public IActionResult Post(OrderDto order)
        {

            var newOrder = new Order
            {
                ClientName = order.ClientName,
                ClientAddress = order.ClientAddress,
                Quantity = order.Quantity,
                Product = this.dbContext.Products.Single(x => x.Id == order.ProductId),
            };

            this.dbContext.Orders.Add(newOrder);
            this.dbContext.SaveChanges();

            order.Id = newOrder.Id;
            this.messageSender.SendRabbitMqMessage(order);
            return Ok();
        }
    }
}