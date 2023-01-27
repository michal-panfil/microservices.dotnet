using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseService.Core.Interfaces;
using WarehouseService.Core.Models;
using WarehouseService.Infrastructure.Data;

namespace WarehouseService.Infrastructure.Serices
{
    public class ShipmentDataManager : IShipmentDataManager
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public ShipmentDataManager(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }
        public void AddShipment(Shipment shipment)
        {
            using var scope = this.serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WarehouseContext>();
            context.Shipments.Add(shipment);
            context.SaveChanges();
          
        }
    }
}
