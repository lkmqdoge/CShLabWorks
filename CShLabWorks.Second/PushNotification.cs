namespace CShLabWorks.Second;

public record PushNotification
{
    public int DeviceId { get; init; }

    public string AppName { get; init; } = string.Empty;
}

