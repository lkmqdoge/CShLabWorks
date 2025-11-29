namespace CShLabWorks.Second;

public sealed record EmailNotification : Notification
{
    public string EmailAddress { get; set; }

    public string Subject { get; set; }
}

