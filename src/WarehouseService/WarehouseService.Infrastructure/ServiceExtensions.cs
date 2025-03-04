﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WarehouseService.Core.Interfaces;
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
            services.AddDbContext<WarehouseContext>(x => x.UseSqlServer(configuration.GetConnectionString("WarehouseDb")));
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
            try
            {
                context.Database.Migrate();
            }
            catch (Exception)
            {

            }
        }

        public static void AddHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<IShipmentClient, ShipmentClient>();
            services.AddScoped<IWarehouseApiClient, WarehouseApiClient>();
        }
    }
}
