namespace CShLabWorks.Second;

public sealed record SmsNotification : Notification
{
    public string PhoneNumber { get; init; } = string.Empty;
}

