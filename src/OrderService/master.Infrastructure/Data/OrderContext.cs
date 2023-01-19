using master.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace master.Infrastructure.Data
{
    public class OrderContext : DbContext
    {
        
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasOne(x => x.Product).WithMany(x=> x.Orders).HasForeignKey(x => x.ProductId);
            modelBuilder.Entity<Product>().Property(x => x.Price).HasColumnType("decimal(18,2)");

        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
