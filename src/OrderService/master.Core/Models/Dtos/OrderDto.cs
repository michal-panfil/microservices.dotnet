using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Core.Models.Dtos
{
    public class OrderDto
    {
        public List<OrderItemDto> Items { get; set; }
        public int CustomerId { get; set; }
    }
}
