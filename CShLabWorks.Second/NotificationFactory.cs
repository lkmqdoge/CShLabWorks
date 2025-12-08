namespace CShLabWorks.Second;

public static class NotificationFactory
{
    public static Func<string, string, EmailNotification> CreateEmailFactory(NotificationPriority priority)
        => (emailAddress, subject) => new EmailNotification
        {
            EmailAddress = emailAddress,
            Subject = subject,
            Priority = priority,
        };

    public static Func<string, SmsNotification> CreateSmsFactory(NotificationPriority priority)
        => (phoneNumber) => new SmsNotification
        {
            PhoneNumber = phoneNumber,
            Priority = priority,
        };

    public static Notification CreateFromTemplate(string template, Dictionary<string, string> parameters)
    {
        var m = template;
        foreach (var pair in parameters)
            m = m.Replace(pair.Key, pair.Value);

        return new Notification()
        {
            Title = parameters.TryGetValue("title", out var t) ? t : "TITLE",
            Message = m
        };
    }
}
