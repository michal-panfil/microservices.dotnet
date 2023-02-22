namespace WarehouseService.Core.Models
{
    public class ShipmentDto
    {
        public int ShipmentId { get; set; }
        public int RemainingKm { get; set; }
        public string CurrentLocation { get; set; }
    }
}
