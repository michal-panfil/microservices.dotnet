using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Core.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public IList<OrderItem> Items { get; set; }

        public Customer Customer { get; set; }
    }
}