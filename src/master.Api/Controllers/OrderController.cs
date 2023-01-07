using master.Core.Models;
using master.Core.Models.Dtos;
using master.Core.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace master.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public List<Order> Get()
        {
            return Enumerable.Range(0, 3).Select(i => new Order
            {
                Id = i,
                Customer = new Customer
                {
                    Id = i,
                    Name = $"Customer {i}"
                },
                Items = Enumerable.Range(0, 3).Select(j => new OrderItem
                {
                    Id = j,
                    ProductId = 1
                }).ToList()
            }).ToList();
        }
        [HttpPost]
        public IActionResult Post(OrderDto order)
        {
            return Ok();
        }
    }
}
