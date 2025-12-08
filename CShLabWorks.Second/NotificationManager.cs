namespace CShLabWorks.Second;

public class NotificationManager
{
    //  возникает при отправке любого оповещения. Получает
    //  отправленное оповещение в качестве параметра
    public event Action<Notification>? NotificationSend;

    // возникает при получении критического оповещения
    public event Action<Notification>? CriticalNotificationSend;

    // событие-фильтр, позволяющее подписчикам отклонять
    // оповещения по некоторому условию
    public event Action<Notification>? NotificationFilter;

    public Func<Notification, bool>? Filter { get; set; }

    // история всех отосланных оповещений
    public List<Notification> NotificationHistory { get; init; } = [];

    // словарь подписчиков, сгруппированных по приоритетам. Каждый подписчик
    // представляет собой делегат, получающий на вход объект-оповещение
    public Dictionary<NotificationPriority,List<Action<Notification>>> Subsribers { get; init; } = [];

    public NotificationManager()
    {
        foreach (var p in Enum.GetValues<NotificationPriority>())
            Subsribers.Add(p, []);
    }

    // подписка на оповещения определенного приоритета. В качестве
    // параметров получает приоритет и функцию-обработчик объекта-оповещения
    public void Subsribe(NotificationPriority priority, Action<Notification> action)
        => Subsribers[priority].Add(action);

    // отписка от оповещений. В качестве параметров получает
    // приоритет и функцию-обработчик объекта-оповещения
    public void Unsubscribe(NotificationPriority priority, Action<Notification> action)
        => Subsribers[priority].Remove(action);

    // отправка полученного в параметрах оповещения всем подписчикам
    public void Send(Notification notification)
    {
        foreach (var pair in Subsribers)
        {
            if (pair.Key == notification.Priority)
                pair.Value.ForEach(a => a(notification));
        }

        NotificationHistory.Add(notification);

        if (notification.Priority is NotificationPriority.Critical)
            CriticalNotificationSend?.Invoke(notification);
        else
            NotificationSend?.Invoke(notification);

        if (Filter?.Invoke(notification) != null)
            NotificationFilter?.Invoke(notification);
    }

    // получение списка оповещений по некоторому условию, 
    // передаваемому в качестве параметра

    // обработка конкретного оповещения. Объект-оповещение
    // и функция обработчик передаются в качестве параметров.
    public void ProccesNotification(Notification notification, Action<Notification> action)
    {
        action(notification);
        NotificationHistory.Add(notification);
    }

    // возвращает словарь с количеством оповещений каждого приоритета;
    public Dictionary<NotificationPriority, int> GetCountByPriotity()
    {
        Dictionary<NotificationPriority, int> res = [];

        foreach(var priority in Enum.GetValues<NotificationPriority>())
        {
            res.Add(priority, Subsribers[priority].Count);
        }

        return res;
    }

    // возвращает перечисление из всех оповещений за
    // последний период времени. Период передается в качестве параметра типа TimeSpan;
    public IEnumerable<Notification> GetRecentNotifications(TimeSpan time)
        => NotificationHistory.Where(n =>
                DateTime.Now
                .Subtract(time)
                .CompareTo(n.TimeStamp) < 0);

    public Dictionary<Type, List<Notification>> GroupByType()
    {
        Dictionary<Type, List<Notification>> res = [];

        res.Add(typeof(SmsNotification), []);
        res.Add(typeof(EmailNotification), []);
        res.Add(typeof(PushNotification), []);
        res.Add(typeof(Notification), []);

        foreach (var notification in NotificationHistory)
        {
            if (notification is SmsNotification sn)
                res[typeof(SmsNotification)].Add(sn);
            else if (notification is EmailNotification en)
                res[typeof(EmailNotification)].Add(en);
            else if (notification is PushNotification pn)
                res[typeof(PushNotification)].Add(pn);
            else
                res[typeof(Notification)].Add(notification);
        }
        return res;
    }

    // вычисляет среднее количество оповещений в указанном интервале времени
    public double GetAverageNotificationsPerHour(DateTime? from = null)
    {
        DateTime startTime = from ?? NotificationHistory.Min(n => n.TimeStamp);
        DateTime endTime = DateTime.Now;

        var notificationsInPeriod = NotificationHistory
            .Where(n => n.TimeStamp >= startTime && n.TimeStamp <= endTime)
            .ToList();

        int count = notificationsInPeriod.Count;
        if (count == 0)
            return 0;

        double totalHours = Math.Ceiling(endTime.Subtract(startTime).TotalHours);
        return count / totalHours;
    }
}

