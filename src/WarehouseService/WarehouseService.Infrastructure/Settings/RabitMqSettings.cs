namespace WarehouseService.Infrastructure.Settings;

public class RabitMqSettings
{
    public string UserName { get; init; }
    public string Password { get; init; }
    public string HostName { get; init; }
    public int Port { get; init; }
    public Dictionary<string, string> Queues { get; init; }
}
