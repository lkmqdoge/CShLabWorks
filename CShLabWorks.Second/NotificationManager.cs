namespace CShLabWorks.Second;

public class NotificationManager
{
    public event Action<Notification> NotificationSend;

    public event Action<Notification> CriticalNotificationSend;

    public event Action<Notification> NotificationFilter;

    public readonly List<Notification> NotificationHistory { get; } = [];

    public readonly Dictionary<NotificationPriority,List<Action<Notification>>> Subsribers { get; } = [];

    public void Subsribe()
    {

    }

    public void Unsubscribe()
    {

    }

    public void Send()
    {

    }

    public List<Notification> GetNotifications()
    {

    }

    public void ProccesNotification()
    {

    }
}

