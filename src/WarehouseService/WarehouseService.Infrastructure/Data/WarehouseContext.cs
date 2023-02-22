using Microsoft.EntityFrameworkCore;

namespace WarehouseService.Infrastructure.Data
{
    public class WarehouseContext : DbContext
    {

        public WarehouseContext(DbContextOptions<WarehouseContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public DbSet<WarehouseService.Core.Models.Shipment> Shipments { get; set; }
    }
}
