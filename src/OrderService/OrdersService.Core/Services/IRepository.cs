using OrdersService.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Core.Services
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll(Predicate<T> predicate);
        Task Insert(T orderToInsert);

    }
}
