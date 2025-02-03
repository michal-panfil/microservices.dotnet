namespace WarehouseService.Core.Models;

public record struct OrderDto(int Id, string ClientName, string ClientAddress, int Quantity, int ProductId, string? ProductName);
