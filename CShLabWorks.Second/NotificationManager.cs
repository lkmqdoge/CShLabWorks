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

    // история всех отосланных оповещений
    public List<Notification> NotificationHistory { get; init; } = [];

    // словарь подписчиков, сгруппированных по приоритетам. Каждый подписчик
    // представляет собой делегат, получающий на вход объект-оповещение
    public Dictionary<NotificationPriority,List<Action<Notification>>> Subsribers { get; init; } = [];

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
            pair.Value.ForEach(a => a(notification));

        NotificationHistory.Add(notification);

        if (notification.Priority.Equals(NotificationPriority.Critical))
            CriticalNotificationSend?.Invoke(notification);
        else
            NotificationSend?.Invoke(notification);
    }

    // получение списка оповещений по некоторому условию, 
    // передаваемому в качестве параметра
    public List<Notification> GetNotifications(Func<Notification, bool> condition)
        => [.. NotificationHistory.Where(condition) ];

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

    public Dictionary<Type, Notification> GroupByType()
    {
        throw new NotImplementedException();
    }

    // вычисляет среднее количество оповещений в указанном интервале времени
    public double GetAverageNotificationsPerHour()
    {
        throw new NotImplementedException();
    }
}

