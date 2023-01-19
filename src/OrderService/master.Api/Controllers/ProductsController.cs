using master.Core.Models.Entities;
using master.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace master.Api.Controllers
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
