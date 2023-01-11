using master.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace master.Infrastructure.Data
{
    public class OrderContext : DbContext
    {
        
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
