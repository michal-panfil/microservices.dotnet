namespace WarehouseService.Core.Models
{
    public class Shipment : BaseEntity
    {
        public int OrderId { get; set; }
        public string Address { get; set; } = string.Empty;
        public string ReciverName { get; set; } = string.Empty;
        public int KmToTarget { get; set; }
    }
}
