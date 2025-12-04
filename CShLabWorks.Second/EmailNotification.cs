namespace CShLabWorks.Second;

public sealed record EmailNotification : Notification
{
    public string EmailAddress { get; init; } = string.Empty;

    public string Subject { get; init; } = string.Empty;
}

