using OrdersService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersService.Core.Interfaces;
using OrdersService.Infrastructure.Services;
using OrdersService.Core.Models.Dtos;
using Microsoft.AspNetCore.Builder;
using OrdersService.Core.Models.Entities;
using OrdersService.Infrastructure.Models;

namespace OrdersService.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(x => x.UseSqlServer("Server=order_mssql_db,1433;Database=OrderDb;User Id=SA;Password=Password12345!;TrustServerCertificate=True;Encrypt=False"));
        }

        public static void AddMessageBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IMessageBusReceiver<UpdateStatusMessage>, MessageBusReceiver<UpdateStatusMessage>>();
            services.AddTransient<MessageBusSender<OrderDto>>();
        }

        public static void AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<OrderDataService>();
        }
        public static void MigrateDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetRequiredService<OrderContext>();
            context.Database.Migrate();
            if (!context.Products.Any())
            {
                context.Products.Add(new Product { Name = "Product 1", Price = 10 });
                context.Products.Add(new Product { Name = "Product 2", Price = 20 });
                context.Products.Add(new Product { Name = "Product 3", Price = 30 });
                context.SaveChanges();
            }
        }
    }
}
