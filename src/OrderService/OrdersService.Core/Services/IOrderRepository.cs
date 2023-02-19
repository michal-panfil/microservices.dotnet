using OrdersService.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Core.Services
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetOrders(Predicate<Order> predicate);
        Task InsertOrder(Order orderToInsert);

    }
}
