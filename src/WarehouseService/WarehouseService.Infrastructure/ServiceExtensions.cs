using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseService.Core.Interfaces;
using WarehouseService.Core.Models.Messages;
using WarehouseService.Core.Services;
using WarehouseService.Infrastructure.Serices;

namespace WarehouseService.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<NewOrderManager>();
            services.AddTransient<IMessageBusSender<UpdateStatusMessage>, MessageBusSender<UpdateStatusMessage>>();
        }
    }
}
