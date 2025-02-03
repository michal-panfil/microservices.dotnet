using WarehouseService.Core.Enums;

namespace WarehouseService.Core.Models.Messages;

public record struct UpdateStatusMessage(int OrderId, OrderStatus Status);
