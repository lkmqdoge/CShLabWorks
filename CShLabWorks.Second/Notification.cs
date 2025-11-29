namespace CShLabWorks.Second;

public record Notification
{
    public int Id;

    public int Type;

    public string Title;

    public string Message;

    public DateTime TimeStamp;

    public NotificationPriority Priority;
}

public enum NotificationPriority
{
    Low,
    Normal,
    High,
    Critical,
}
