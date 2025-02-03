namespace WarehouseService.Core.Models;

public record struct ShipmentDto( int ShipmentId, int RemainingKm, string CurrentLocation );
