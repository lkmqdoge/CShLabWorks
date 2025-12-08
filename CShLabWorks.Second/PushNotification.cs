namespace CShLabWorks.Second;

public sealed record PushNotification : Notification
{
    public int DeviceId { get; init; }

    public string AppName { get; init; } = string.Empty;
}

