using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseService.Core.Interfaces;
using WarehouseService.Core.Models;
using WarehouseService.Core.Models.Messages;
using WarehouseService.Core.Services;
using WarehouseService.Infrastructure.Data;
using WarehouseService.Infrastructure.Serices;

namespace WarehouseService.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WarehouseContext>(x => x.UseSqlServer("Server=warehouse_mssql_db,1433;Database=WarehouseDb;User Id=SA;Password=Password12345!;TrustServerCertificate=True;Encrypt=False"));
            services.AddScoped(typeof(IWarehouseRepository<>), typeof(WarehouseRepository<>));
        }
        
        public static void AddDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<NewOrderManager>();
            services.AddTransient<IMessageBusSender<UpdateStatusMessage>, MessageBusSender<UpdateStatusMessage>>();
            services.AddTransient<IShipmentDataManager, ShipmentDataManager>();

        }
        public static void AddSignalRServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IShipmentClient, ShipmentClient>();
        }
        public static void MigrateDatabase(this IServiceCollection sercvices)
        {
            using var serviceScope = sercvices.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetRequiredService<WarehouseContext>();
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }
    }
}
