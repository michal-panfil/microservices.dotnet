using OrdersService.Core.Models.Enums;
using OrdersService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Infrastructure.Services
{
    public class OrderDataService
    {
        private readonly OrderContext context;

        public OrderDataService(OrderContext context)
        {
            this.context = context;
        }
        public void UpdataOrderStatus(int orderId, OrderStatus status)
        {
            var order = context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                order.Status = status;
                context.SaveChanges();
            }
        }
    }
}
