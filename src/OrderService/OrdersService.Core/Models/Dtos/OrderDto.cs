using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Core.Models.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }

        public string? Status { get; set; }
    }
}
