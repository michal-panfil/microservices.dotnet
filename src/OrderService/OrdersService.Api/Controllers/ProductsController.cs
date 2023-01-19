using OrdersService.Core.Models.Entities;
using OrdersService.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrdersService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController(OrderContext dbContext)
        {
            DbContext = dbContext;
        }

        public OrderContext DbContext { get; }
        [HttpGet]
        public List<Product> Get()
        {
            return this.DbContext.Products.ToList();
        }
    }
}
