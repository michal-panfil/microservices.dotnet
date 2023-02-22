using WarehouseService.Core.Models;

namespace WarehouseService.Core.Interfaces
{
    public interface IWarehouseRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        Task<T> GetAsync(int id);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
