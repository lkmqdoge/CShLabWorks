namespace CShLabWorks.Second;

public record Notification
{
    public int Id { get; init;}

    public int Type { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Message { get; init;} = string.Empty;

    public DateTime TimeStamp { get; init; } = DateTime.Now;

    public NotificationPriority Priority { get; init; }
}

