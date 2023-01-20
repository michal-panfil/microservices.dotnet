﻿using OrdersService.Infrastructure.Data;
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

namespace OrdersService.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void AddMessageBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IMessageBusReceiver, MessageBusReceiver>();
            services.AddTransient<MessageBusSender<OrderDto>>();


        }
    }
}
