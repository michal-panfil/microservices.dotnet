namespace WarehouseService.Core.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
    }
}
