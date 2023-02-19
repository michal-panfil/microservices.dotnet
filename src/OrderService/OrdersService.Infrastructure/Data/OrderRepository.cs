using OrdersService.Core.Models.Entities;
using OrdersService.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Infrastructure.Data;
public class OrderRepository : IOrderRepository
{
    private readonly OrderContext context;
    public OrderRepository(OrderContext dbContext)
    {
        context = dbContext;
    }
    public IQueryable<Order> GetOrders(Predicate<Order> predicate)
    {
        return this.context.Orders.Where(x => predicate(x));
    }
    public async Task InsertOrder(Order orderToInsert) => await this.context.Orders.AddAsync(orderToInsert);

}
