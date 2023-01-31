using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using WarehouseService.Core.Models;

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
