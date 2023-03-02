using Microsoft.EntityFrameworkCore;
using OrdersService.Core.Models.Entities;
using OrdersService.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Infrastructure.Data;
public class OrderRepository<TEntity> where TEntity : class
{
    private readonly OrderContext context;
    public OrderRepository(OrderContext dbContext)
    {
        context = dbContext;
    }
    public IQueryable<TEntity> GetOrders(Predicate<TEntity> predicate)
    {
        return this.context.Set<TEntity>();
    }
    public async Task InsertOrder(TEntity orderToInsert)
    {
        await this.context.Set<TEntity>().AddAsync(orderToInsert);
        await this.context.SaveChangesAsync();
    }

}
