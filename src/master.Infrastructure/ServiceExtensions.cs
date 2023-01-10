using master.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddDatabase(IConfiguration configuration, IServiceCollection services)
        {
            services.AddDbContext<OrderContext>(x => x.UseSqlite(@"Data Source=C:\db\orderingMaster.db"));
        }
    }
}
