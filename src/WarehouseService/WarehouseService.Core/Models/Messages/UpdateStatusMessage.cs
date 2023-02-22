using WarehouseService.Core.Enums;

namespace WarehouseService.Core.Models.Messages
{
    public class UpdateStatusMessage
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
