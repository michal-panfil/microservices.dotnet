using Microsoft.EntityFrameworkCore;
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
    public class WarehouseRepository<T> : IWarehouseRepository<T> where T : BaseEntity
    {
        private readonly WarehouseContext context;
        private readonly DbSet<T> entities;

        public WarehouseRepository(WarehouseContext context)
        {
            this.context = context;
            this.entities = context.Set<T>();
        }

        public async Task<T?> GetAsync(int id)
        {
            if (entities == null)
            {
                return null;
            }

            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }
        
        public IQueryable<T> GetAll()
        {
            return entities.AsQueryable();
        }

        public async Task InsertAsync(T entity)
        {
            entities.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(T entity)
        {
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
